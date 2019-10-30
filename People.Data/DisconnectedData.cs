using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PeopleInformation.Domain;
using PeopleInformation.Domain.AntiCorruption.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleInformation.Data
{
    public class DisconnectedData
    {
        private PeopleInformationContext _context;

        public DisconnectedData(PeopleInformationContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public List<Object> GetCustomerList()
        {
            var users = _context.Users.ToList();
            var output = new List<Object>();
            foreach (var user in users)
            {
                output.Add(new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.IsAdmin
                });
            };
            return output;
        }

        public object LoadUserGraph(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).Select(u => new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.IsAdmin,
                u.Password,
                u.Phone,
                DateOfBirth = u.DateOfBirth.ToString("M/dd/yyyy"),
                u.Gender,
                Addresses = u.Addresses.Select(a => new
                {
                    a.Id,
                    a.Street,
                    a.City,
                    a.County,
                    a.Zip,
                    a.State,
                    a.Country,
                    a.MoveInDate,
                    a.MoveOutDate
                }),
                    Cases = u.Cases.Select(c => new
                    {
                        c.Id,
                        c.Subject,
                        DateTimeOfInitialMessage = c.DateTimeOfInitialMessage.ToLocalTime().ToString("MM-dd-yyyy hh:mm:ss tt"),
                        TimeToResolution = c.TimeToResolution.HasValue ? c.TimeToResolution.Value.ToLocalTime().ToString("MM-dd-yyyy hh:mm:ss tt") : string.Empty,
                        Messages = c.Messages.Select(m => new
                        {
                            m.Id,
                            m.MessageText,
                            m.OriginatorId,
                            m.DateTimeOfMessage
                    })
                })
            }).FirstOrDefault();

            return user ?? null;
        }

        public UserProfile LoadUserProfile(int id)
        {
            var userProfile = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userProfile != null)
            {
                var output = new UserProfile
                {
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    Email = userProfile.Email,
                    Password = userProfile.Password,
                    Phone = userProfile.Phone,
                    DateOfBirth = userProfile.DateOfBirth == DateTime.MinValue ? "" : userProfile.DateOfBirth.ToShortDateString()
                };
                return output;
            }
            return null;
        }

        public List<Address> LoadUserAddresses(int id)
        {
            if (_context.Users.Any(a => a.Id == id))
            {
                return _context.Addresses.Where(a => a.UserId == id).ToList();
            }
            return null;
        }

        public User HandleLogin(User model)
        {
            var user = _context.Users.Where(u => u.Email == model.Email).FirstOrDefault();
            if (user != null && user.Password == model.Password) return user;
            else return null;
        }

        public Address LoadAddress(int userId, int addressId)
        {
            if (_context.Addresses.Any(a => a.Id == addressId && a.UserId == userId))
            {
                var address = _context.Addresses.Where(a => a.Id == addressId).FirstOrDefault();
                return address;
            }
            return null;
        }

        public bool HandleAddressSubmission(Dictionary<string, string> userClaims, Address address)
        {
            //var userDbObject = _context.Users.Where(e => e.Email == userClaims["Email"]).FirstOrDefault();
            var doesUserAddressAlreadyExist = _context.Addresses.Any(a => a.UserId == int.Parse(userClaims["id"])
                && a.Street == address.Street
                && a.City == address.City
                && a.County == address.County
                && a.Zip == address.Zip
                && a.State == address.State
                && a.Country == address.Country
                && a.MoveInDate == address.MoveInDate);
            //var doesUserAddressAlreadyExist = userDbObject.Addresses.Any(a => a.Street == address.Street);
            if (!doesUserAddressAlreadyExist)
            {
                var mappedAddress = new Address
                {
                    Street = address.Street,
                    City = address.City,
                    County = address.County,
                    Zip = address.Zip,
                    State = address.State,
                    Country = address.Country,
                    MoveInDate = address.MoveInDate,
                    MoveOutDate = address.MoveOutDate,
                    UserId = int.Parse(userClaims["id"])
                };
                _context.Entry(mappedAddress).State = EntityState.Added;
                _context.SaveChanges();
                // true represents changes made to userDbObject
                return true;
            }
            // false represents no changes made to userDbObject because address already existed
            return false;
        }

        public bool HandleAddressUpdate(Dictionary<string, string> userClaims, Address address)
        {
            if (_context.Users.Any(u => u.Id == int.Parse(userClaims["id"]) && _context.Addresses.Any(a => a.Id == address.Id)))
            {
                var addressDBObject = _context.Addresses.Where(a => a.Id == address.Id).FirstOrDefault();
                var areCustomerSubmissionAndDbEqual = (addressDBObject.Street == address.Street
                    && addressDBObject.City == address.City
                    && addressDBObject.County == address.County
                    && addressDBObject.Zip == address.Zip
                    && addressDBObject.State == address.State
                    && addressDBObject.Country == address.Country
                    && addressDBObject.MoveOutDate == address.MoveOutDate
                    && addressDBObject.MoveInDate == address.MoveOutDate) ? true : false;
                if (!areCustomerSubmissionAndDbEqual)
                {
                    var modifiedAddress = new Address
                    {
                        Street = address.Street,
                        City = address.Street,
                        Country = address.Country,
                        County = address.County,
                        Zip = address.Zip,
                        State = address.State,
                        MoveInDate = address.MoveInDate,
                        MoveOutDate = address.MoveOutDate,
                        Id = address.Id,
                        UserId = int.Parse(userClaims["id"])
                        };

                    _context.Entry(modifiedAddress).State = EntityState.Modified;
                    _context.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public User HandleUserRegistration(User model)
        {
            var existingAccount = doesAccountExist(model.Email);
            if (!existingAccount)
            {
                var pendingUser = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    IsAdmin = false,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = DateTime.MinValue,
                    Phone = "",
                    Gender = ""
                };
                SaveCustomerGraph(pendingUser);
                var user = _context.Users.Where(u => u.Email == pendingUser.Email).FirstOrDefault();
                return user;
            }
            else return null;
        }

        public User HandleUserProfile(Dictionary<string, string> userClaims, UserProfile data)
        {
            var userDbObject = _context.Users.Where(e => e.Email == userClaims["Email"]).FirstOrDefault();
            var normalizedDob = $"{userDbObject.DateOfBirth.Day}/{userDbObject.DateOfBirth.Month}/{userDbObject.DateOfBirth.Year}";
            var areCustomerSubmissionAndDbEqual = userDbObject.FirstName == data.FirstName
                && userDbObject.LastName == data.LastName
                && userDbObject.Email == data.Email
                && userDbObject.Password == data.Password
                && userDbObject.Phone == data.Phone
                && DateTime.Parse(normalizedDob) == DateTime.Parse(data.DateOfBirth);

            if (!areCustomerSubmissionAndDbEqual)
            {
                var dataMapped = new User()
                {
                    Id = userDbObject.Id,
                    IsAdmin = userDbObject.IsAdmin,
                    Gender = userDbObject.Gender,
                    Addresses = userDbObject.Addresses,
                    Cases = userDbObject.Cases,
                    IsDirty = true,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Password = data.Password,
                    Phone = data.Phone,
                    DateOfBirth = DateTime.Parse(data.DateOfBirth)
                };
                SaveCustomerGraph(dataMapped);
                return dataMapped;
            }
            return null;
        }

        public bool doesAccountExist(string email)
        {
            // if user does not exist return true
            return _context.Users.FirstOrDefault(u => u.Email.ToString().ToLower() == email.ToLower()) == null ? false : true;
        }

        public void SaveCustomerGraph(User user)
        {
            _context.ChangeTracker.TrackGraph(user, c => ApplyStateUsingIsKeySet(c.Entry));
            _context.SaveChanges();
        }

        private static void ApplyStateUsingIsKeySet(EntityEntry entry)
        {
            if (entry.IsKeySet)
            {
                if (((ClientChangeTracker)entry.Entity).IsDirty)
                {
                    entry.State = EntityState.Modified;
                }
                else
                {
                    entry.State = EntityState.Unchanged;
                }
            }
            else
            {
                entry.State = EntityState.Added;
            }
        }

        //EF Core supports Cascade delete by convention
        //Even if full graph is not in memory, db is defined to delete
        //But always double check!
        public bool DeleteCustomerGraph(int id)
        {
            if (_context.Users.Any(c => c.Id == id))
            {
                var user = _context.Users.Find(id);
                _context.Entry(user).State = EntityState.Deleted;
                _context.SaveChanges();
                // if the user was successfully deleted true is returned, else false for failure
                return _context.Users.Any(c => c.Id != id) ? true : false;
            }
            // if true is returned the user has already been deleted
            return true;
        }

        public string DeleteAddress(int userClaimsId, int addressId)
        {
            if (_context.Addresses.Any(a => a.UserId == userClaimsId))
            {
                var address = _context.Addresses.Find(addressId);
                _context.Entry(address).State = EntityState.Deleted;
                _context.SaveChanges();
                // if the user was successfully deleted true is returned, else false for failure
                return _context.Addresses.Any(a => a.Id != addressId) ? "true" : "false";
            }
            return "unauthorized";
        }

        public List<UserCase> GetUserCases(int id)
        {
            if (_context.Cases.Any(c => c.UserId == id))
            {
                var cases = _context.Cases.Where(c => c.UserId == id).ToList();
                var output = new List<UserCase>();
                foreach (var item in cases)
                {
                    output.Add(
                        new UserCase
                        {
                            Id = item.Id,
                            ClosedById = item.ClosedById,
                            Subject = item.Subject,
                            UserId = item.UserId,
                            DateTimeOfInitialMessage = item.DateTimeOfInitialMessage.ToLocalTime().ToString("MM-dd-yyyy hh:mm:ss tt"),
                            TimeToResolution = item.TimeToResolution.HasValue ? item.TimeToResolution.Value.ToLocalTime().ToString("MM-dd-yyyy hh:mm:ss tt") : string.Empty
                        });
                }
                    return output;
            }
                return null;
        }

        public bool HandleCloseUserCase(int id, int caseId)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            var caseObject = _context.Cases.Where(c => c.Id == caseId).FirstOrDefault();
            if ((id == caseObject.UserId || user.IsAdmin == true) && caseObject.TimeToResolution == null)
            {
                var modifiedCaseObject = new Case
                {
                    ClosedById = id,
                    Id = caseObject.Id,
                    DateTimeOfInitialMessage = caseObject.DateTimeOfInitialMessage,
                    Subject = caseObject.Subject,
                    UserId = id,
                    TimeToResolution = DateTime.UtcNow,
                    User = caseObject.User
                };
                _context.Entry(modifiedCaseObject).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Object GetUserCaseAndMessages(int caseId, int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (_context.Cases.Any(c => c.Id == caseId && (c.UserId == id) || user.IsAdmin == true))
            {
                var output = _context.Cases.Where(c => c.Id == caseId).Select(x => new
                {
                    x.Id,
                    x.Subject,
                    DateTimeOfInitialMessage = x.DateTimeOfInitialMessage.ToLocalTime().ToString("MM-dd-yyyy hh:mm:ss tt"),
                    TimeToResolution = x.TimeToResolution.HasValue ? x.TimeToResolution.Value.ToString("MM-dd-yyyy hh:mm:ss tt") : string.Empty,
                    x.ClosedById,
                    Messages = x.Messages.Select(m => new
                    {
                        m.Id,
                        DateTimeOfMessage = m.DateTimeOfMessage.ToLocalTime().ToString("MM-dd-yyyy hh:mm:ss tt"),
                        m.MessageText,
                        m.OriginatorId
                    })
                }).FirstOrDefault();
                return output ?? null;
            }
            return null;
        }

        public bool HandleNewMessageSubmission(int caseId, int userId, Message message)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var caseObject = _context.Cases.Where(c => c.Id == caseId).FirstOrDefault();
            if (caseObject != null
                && user != null
                && message.MessageText != null
                && caseObject.TimeToResolution == null
                && (caseObject.UserId == userId || user.IsAdmin == true))
            {
                var newMessage = new Message
                {
                    MessageText = message.MessageText,
                    CaseId = caseId,
                    OriginatorId = userId
                };
                _context.Entry(newMessage).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Case HandleCreateNewUserCase(int userId, Case caseInfo)
        {
            if (_context.Users.Any(u => u.Id == userId))
            {
                var newCase = new Case
                {
                    UserId = userId,
                    Subject = caseInfo.Subject
                };
                _context.Entry(newCase).State = EntityState.Added;
                _context.SaveChanges();
                var output = _context.Cases.Where(c => c.Subject == caseInfo.Subject).FirstOrDefault();
                if (output != null) return output;
                return null;
            }
            return null;
        }
    }

    public static class AgeInYearsConverter
    {
        public static int ConvertToAgeInYears(this DateTime DOB)
        {
            var currentDate = DateTime.Now;

            int AgeInYears = currentDate.Year - DOB.Year;
            if (currentDate.Month < DOB.Month && currentDate.Month == DOB.Month && currentDate.Day < DOB.Day)
            {
                AgeInYears--;
            }
            return AgeInYears;
        }
    }
}
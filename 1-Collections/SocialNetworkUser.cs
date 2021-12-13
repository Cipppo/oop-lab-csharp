using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private IDictionary<String, List<TUser>> map;

        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            map = new Dictionary<string, List<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            foreach (var listedUser in this.map[group])
            {
                if (user.Username == listedUser.Username)
                {
                    return false;
                }
            }
            this.map[group].Add(user);
            return true;
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                IList<TUser> friends = new List<TUser>();
                foreach (var entry in this.map.Keys)
                {
                    foreach (var listedUser in this.map[entry])
                    {
                        if (!friends.Contains(listedUser))
                        {
                            friends.Add(listedUser);
                        }
                    }
                }

                return friends;
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            throw new NotImplementedException("TODO construct and return a collection containing of all users followed by the current users, in group");
        }
    }
}

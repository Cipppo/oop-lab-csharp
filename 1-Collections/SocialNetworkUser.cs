using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

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
            if (!this.map.Keys.Contains(group))
            {
                var friend = new List<TUser>();
                friend.Add(user);
                this.map.Add(group, friend);
            }
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
                var friends = new List<TUser>();
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
            var friends = new List<TUser>();
            foreach (var friend in this.map[group])
            {
                friends.Add(friend);
            }

            return friends;

        }
    }
}

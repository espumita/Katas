﻿namespace TripService.User;

public class User {
    private List<Trip.Trip> trips = new List<Trip.Trip>();
    private List<User> friends = new List<User>();

    public List<User> GetFriends() {
        return friends;
    }

    public void AddFriend(User user) {
        friends.Add(user);
    }

    public void AddTrip(Trip.Trip trip) {
        trips.Add(trip);
    }

    public List<Trip.Trip> Trips() {
        return trips;
    }

    public bool IsFriendOf(User user) {
        return user.GetFriends()
            .Any(friend => friend.Equals(this));
    }
}
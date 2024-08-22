using FluentAssertions;
using TripService.Exception;

namespace TripService.Tests;

public class TripServiceTests {

    [Test]
    public void user_has_no_trips() {
        var tripService = new TripServiceForTest();
        var aUser = new User.User();
        tripService.GivenALoggedUser(aUser);
        tripService.GivenTripsWhenFindByUser(new List<Trip.Trip>());
        
        var trips = tripService.GetTripsByUser(aUser);

        trips.Should().BeEmpty();
    }

    [Test]
    public void trow_exception_is_user_is_not_logged() {
        var tripService = new TripServiceForTest();
        var aUser = new User.User();
        tripService.GivenALoggedUser(null);

        Action action = () => tripService.GetTripsByUser(aUser);

        action.Should().Throw<UserNotLoggedInException>();
    }

    [Test]
    public void do_not_return_trips_when_user_is_not_a_friend() {
        var tripService = new TripServiceForTest();
        var aUser = new User.User();
        var anotherUser = new User.User();
        tripService.GivenALoggedUser(aUser);
        tripService.GivenTripsWhenFindByUser(new List<Trip.Trip>{ new Trip.Trip() });

        var trips = tripService.GetTripsByUser(anotherUser);

        trips.Should().BeEmpty();
    }

    [Test]
    public void return_trips_when_user_is_a_friend() {
        var tripService = new TripServiceForTest();
        var aUser = new User.User();
        var anotherUser = new User.User();
        anotherUser.AddFriend(aUser);
        tripService.GivenALoggedUser(aUser);
        tripService.GivenTripsWhenFindByUser(new List<Trip.Trip> { new Trip.Trip() });

        var trips = tripService.GetTripsByUser(anotherUser);

        trips.Count().Should().Be(1);
    }
}

public class TripServiceForTest : Trip.TripService {
    private User.User loggedUser;
    private List<Trip.Trip> tripsWhenFindByUser;

    public void GivenALoggedUser(User.User loggedUser) {
        this.loggedUser = loggedUser;
    }

    public void GivenTripsWhenFindByUser(List<Trip.Trip> tripsWhenFindByUser) {
        this.tripsWhenFindByUser = tripsWhenFindByUser;
    }

    protected override User.User LoggedUser() {
        return loggedUser;
    }

    protected override List<Trip.Trip> FindTripsByUser(User.User user) {
        return tripsWhenFindByUser;
    }
}
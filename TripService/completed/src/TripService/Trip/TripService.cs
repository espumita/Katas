using TripService.Exception;
using TripService.User;

namespace TripService.Trip;

public class TripService {
    public List<Trip> GetTripsByUser(User.User user) {
        var loggedUser = LoggedUser();
        CheckIfUserIsLogged(loggedUser);
        return loggedUser.IsFriendOf(user)
            ? FindTripsByUser(user)
            : new List<Trip>();
    }

    protected virtual User.User LoggedUser() {
        return UserSession.GetInstance().GetLoggedUser();
    }

    protected virtual List<Trip> FindTripsByUser(User.User user) {
        return TripDAO.FindTripsByUser(user);
    }

    private void CheckIfUserIsLogged(User.User loggedUser) {
        if (loggedUser == null) throw new UserNotLoggedInException();
    }
}
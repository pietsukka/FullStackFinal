// TODO Get user
// TODO Add User
// TODO Remove User

// TODO Get Tasks
// TODO Add Task
// TODO Remove Task


public static partial class Database {
    

    public static User? GetUser(string username, string passwordHash) {
        try {
            throw new NotImplementedException();
        } catch (Exception) {
            return null;
        }
    }
    public static Task<bool> AddUser(User user) {
        throw new NotImplementedException();
    }
    public static Task<bool> RemoveUser(User user) {
        throw new NotImplementedException();
    }



    public static Task<Task> GetTask() {
        throw new NotImplementedException();
    }
}



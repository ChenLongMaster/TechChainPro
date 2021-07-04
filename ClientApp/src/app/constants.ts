export class Constants {
    public static ClientRoot = window.location.origin + '/';
    public static RootApiUrl = 'https://localhost:5001/';
    public static UserServiceApiUrl = () => `${Constants.RootApiUrl}api/User`;
    public static ContentServiceApiUrl = () => `${Constants.RootApiUrl}api/User/GetUsers`;
}
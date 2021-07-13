export class Constants {
    //Common
    public static ClientRoot = window.location.origin + '/';
    public static RootApiUrl = 'https://localhost:5001/';
    //APIs
    public static UserServiceApiUrl = () => `${Constants.RootApiUrl}api/User`;
    public static ArticleServiceApiUrl = () => `${Constants.RootApiUrl}api/Article`;
}
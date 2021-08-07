export class Constants {
    public static GoogleClientId = '78230486692-tdtq34q2759nvu3edp9vosthqbmreds1.apps.googleusercontent.com';
    public static FacebookClientId = '578655093273196'
    //Common
    public static ClientRoot = window.location.origin + '/';
    public static RootApiUrl = 'https://localhost:5001/';
    //APIs
    public static AuthenticationServiceApiUrl = () => `${Constants.RootApiUrl}api/authentication`;
    public static UserServiceApiUrl = () => `${Constants.RootApiUrl}api/user`;
    public static ArticleServiceApiUrl = () => `${Constants.RootApiUrl}api/article`;
    public static CommonServiceApiUrl = () => `${Constants.RootApiUrl}api/common`;
    public static GetImageUrl = () => `${Constants.RootApiUrl}/images/`;
}
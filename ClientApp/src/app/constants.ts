import { environment } from "src/environments/environment";

export class Constants {
    //Social
    public static GoogleClientId = '78230486692-tdtq34q2759nvu3edp9vosthqbmreds1.apps.googleusercontent.com';
    public static FacebookClientId = '578655093273196'
    //Common
    public static UserDefaultImage = 'assets/Images/user.png';
    public static ArticleEmptyImage = '/assets/Images/placeholder.jpg';
    public static ClientRoot = window.location.origin + '/';
    //APIs
    public static RootApiUrl = environment.rootApiUrl;
    public static AuthenticationServiceApiUrl = () => `${Constants.RootApiUrl}api/authentication`;
    public static UserServiceApiUrl = () => `${Constants.RootApiUrl}api/user`;
    public static ArticleServiceApiUrl = () => `${Constants.RootApiUrl}api/article`;
    public static CommonServiceApiUrl = () => `${Constants.RootApiUrl}api/common`;
    public static GetImageUrl = () => `${Constants.RootApiUrl}/images/`;
}
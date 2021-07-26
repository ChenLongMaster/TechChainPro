export class Constants {
    //Common
    public static ClientRoot = window.location.origin + '/';
    public static RootApiUrl = 'https://localhost:5001/';
    //APIs
    public static UserServiceApiUrl = () => `${Constants.RootApiUrl}api/user`;
    public static ArticleServiceApiUrl = () => `${Constants.RootApiUrl}api/article`;
    public static UploadServiceApiUrl = () => `${Constants.RootApiUrl}api/upload`;
    public static GetImageUrl = () => `${Constants.RootApiUrl}/images/`;
}
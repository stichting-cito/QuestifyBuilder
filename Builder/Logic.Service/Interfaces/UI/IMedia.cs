namespace Questify.Builder.Logic.Service.Interfaces.UI
{
    public interface IMedia
    {
        bool CanAddImage { get; }
        bool CanAddVideo { get; }
        bool CanAddAudio { get; }
        void AddImage();
        void AddVideo();
        void AddAudio();

    }
}

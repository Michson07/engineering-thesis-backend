namespace Users.Application.Queries.GetUserByPhoto
{
    public class GetUserByPhotoView
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
    }
}

namespace BookStoreApi.DTOS
{
    public class SignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public static class SignUpDTOsExtensions
    {
        public static User ToModel(this SignUpDto signUpDto)
        {
            return new User
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                Email = signUpDto.Email,
                UserName = signUpDto.Email,

            };
        }
    }
}

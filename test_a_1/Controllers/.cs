Project: To create a REST API for managing user profiles
User Stories:

1. As a user, I want to be able to create my profile 
2. As a user, I want to be able to update my profile 
3. As a user, I want to be able to delete my profile 

Using .NET Core, the following code can be used to create a basic REST API for managing user profiles:

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace test_a_1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Declare a list to store the user profile
        private static List<User> userProfileList = new List<User>();

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userProfileList;
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User user)
        {
            userProfileList.Add(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            userProfileList[id] = user;
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userProfileList.RemoveAt(id);
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ProfilePictureUrl { get; set; }
    }

}
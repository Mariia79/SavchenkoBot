using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAzureAPI.Models {
    public static class UsersState {
        public static Dictionary<int, State> userStates 
                = new Dictionary<int, State>();
    }
}

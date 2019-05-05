using System.Collections.Generic;

namespace MacroDB.Response{
    public class AdminPostResponse{
        public string token {get; set;}
        public List<NutrientGetResponse> nutrients {get; set;}
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PostManager.DL
{
    public class JsonManager
    {
        public List<Models.post> PostsList;
        public List<Models.Comments> CommentsList;

        public string postsFileName = "posts.json";
        public string commentsFileName = "comments.json";
        public void Initializer()
        {
            PostsList = new List<Models.post>();
            CommentsList = new List<Models.Comments>();
        }

        public bool LoadComments(string commentsFileNameAndPath)
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(commentsFileNameAndPath))
            {
                string json = r.ReadToEnd();
                this.CommentsList = JsonConvert.DeserializeObject<List<Models.Comments>>(json);
            }
            return CommentsList.Count() > 0;
        }

        public bool LoadPosts(string postsFileNameAndPath)
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(postsFileNameAndPath))
            {
                string json = r.ReadToEnd();
                this.PostsList = JsonConvert.DeserializeObject<List<Models.post>>(json);
            }
            return PostsList.Count() > 0;
        }

        public bool PersistComments(string commentsFileNameAndPath) {
            string json = JsonConvert.SerializeObject(CommentsList);
            try
            {
                System.IO.File.WriteAllText(@commentsFileNameAndPath, json);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PersistPosts(string postsFileNameAndPath)
        {
            string json = JsonConvert.SerializeObject(PostsList);
            try
            {
                System.IO.File.WriteAllText(@postsFileNameAndPath, json);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string retrieveAllPosts()
        {
            if (PostsList.Count > 0) {
                return JsonConvert.SerializeObject(PostsList);
            }
            return null;
        }

        public string retrieveAllComments()
        {
            if (PostsList.Count > 0)
            {
                return JsonConvert.SerializeObject(CommentsList);
            }
            return null;
        }

        public string retrieveAPostWithComments(int postId) {
            try
            {
                Models.post postFound = PostsList.Find(x => x.id.Equals(postId));
                if (postFound is null)
                {
                    return string.Empty;
                }
                string partial = JsonConvert.SerializeObject(postFound);
                var commentsFound = CommentsList.Find(x => x.postId.Equals(postId));
                string comments = JsonConvert.SerializeObject(commentsFound);
                if (!(comments is null))
                {
                    comments = string.Empty;                    
                }
                return string.Format("Post:{0} \\r\\n Comments:{1}", partial, comments);

            }
            catch {
                return null;
            }            
        }
    }
}
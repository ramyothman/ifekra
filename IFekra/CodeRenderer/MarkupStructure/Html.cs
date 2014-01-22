using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CodeRenderer.MarkupStructure;

namespace CodeRenderer.MarkupStructure
{
    public class Html
    {
        public Tag RootTag;
        InputReader reader;
        string[] tokens;

        public Html(string InputFilePath)
        {
            reader = new InputReader(InputFilePath, 
                new string[] { "<", ">", "</", "/>", "=", "\"", "\'", ";", ":", " ", "\t" });
            List<string> TokensList = new List<string>();
            while (reader.hasMoreTokens())
                TokensList.Add(reader.Next());
            reader.Close();
            tokens = TokensList.ToArray();
            /*for (int i = 0; i < tokens.Length; i++)
                Console.WriteLine(tokens[i]);*/
            BuildTagTree();
        }
        void BuildTagTree()
        {
            //handle <br/> and attributes
            RootTag = null;
            Stack<Tag> tags = new Stack<Tag>() ;
            
            for (int i = 0; i < tokens.Length; )
            {
                if (tokens[i] == "<")
                {
                    for (i++; tokens[i].Trim().Length == 0; i++) ;   // Skip whitespaces till name of tag
                    Tag tag = new Tag(tokens[i]);
                    if (tags.Count > 0)
                        tags.Peek().Content.Add(tag);

                    List<string> NonParsedAttributes = new List<string>();
                    for (i++; tokens[i] != ">" && tokens[i] != "/>"; i++)
                        NonParsedAttributes.Add(tokens[i]);
                    tag.Attributes = TagAttribute.ParseAttributes(NonParsedAttributes);

                    if (tokens[i] == ">")
                    {
                        tags.Push(tag);
                        if (RootTag == null)
                            RootTag = tag;
                    }
                    i++;
                }
                else if (tokens[i] == "</")
                {
                    while (tokens[i++] != ">") ;    // Skip all till closing the tag
                    tags.Pop();
                }
                else
                {
                    if (tokens[i].Trim().Length > 0)    // Add only non-whitespace text
                        tags.Peek().Content.Add(new Text(tokens[i]));
                    i++;
                }
            }
        }

        #region Printing
        public void PrintTagTree() 
        {
            RootTag.Print();
            PrintTagTree(RootTag,1); 
        }
        private void PrintTagTree(Tag tag,int depth)
        {
            foreach (Token token in tag.Content)
                if (token.type == TokenType.TEXT)
                    token.Print(depth);
                else
                {
                    token.Print(depth);
                    foreach (TagAttribute attribute in ((Tag)token).Attributes)
                        attribute.Print(depth);
                    PrintTagTree((Tag)token, depth + 1);
                }
            return;
        }
        #endregion

        
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide_Project.Models
{
    public class Repository
    {
        //Tempoary Mockup data. This will be exchanged with real database access later on.
        private List<Book> bookList;

        private List<Author> authorList;

        private List<Admin> adminList;

        public Repository()
        {
            bookList = new List<Book>
            {
                new Book {
                    ISBN = "978-0007488308",
                    Title = "Fellowship of the Ring",
                    PublicationYear = 1954,
                    Description = "This is a short description for testing",
                    //"The Fellowship Of The Ring commences with a birthday party in Hobbiton. Bilbo Baggins, the current possessor of the Ring, turns 111 and bequeaths the ring to his cousin, Frodo Baggins. Several years pass and the wizard Gandalf routinely visits Frodo in Bag End. One spring night, he tells Frodo the truth about the Ring and the story behind it. He informs him that Sauron has risen again and is in search of the Ring. Upon learning this, Frodo departs from the Shire alongside three Hobbit friends: Sam, Merry and Pippin. Along the way, the four of them are faced with several obstacles and distractions, including Ringwraiths, who are the servants of Sauron, a malevolent willow tree and an evil tomb ghost. The group befriends wandering Elves on the way and set upon their path across Middle-earth to the Cracks of Doom, intending to destroy the Ring and foil the Dark Lords plan."
                    Pages = 576,
                    BookAuthor = new Author { ID = "1", FirstName = "J.R.R", LastName = "Tolkien", BirthYear = 1892},
                    BookGenre = new Genre { Name = "Fantasy", Art = null } },
                new Book {
                    ISBN = "978-0007488322",
                    Title = "The Two Towers",
                    PublicationYear = 1954,
                    Description = "Continuing the story of The Hobbit, this is the second part of Tolkien’s epic masterpiece, The Lord of the Rings. Frodo and the Companions of the Ring have been beset by danger during their quest to prevent the ruling ring from falling into the hands of the Dark Lord by destroying it in the cracks of doom. They have lost the wizard, Gandalf, in the battle with an evil spirit in the Mines of Moria and at the Falls of Rauros, Boromir, seduced by the power of the Ring, tried to seize it by force. While Frodo and Sam made their escape the rest of the company were attacked by Orcs. Now they continue their journey alone down the great River Anduin – alone, that is, save for the mysterious creeping figure that follows wherever they go.",
                    Pages = 971,
                    BookAuthor = new Author { ID = "1", FirstName = "J.R.R", LastName = "Tolkien", BirthYear = 1892},
                    BookGenre = new Genre { Name = "Fantasy", Art = null } },
                new Book {
                    ISBN = "978-0007488346",
                    Title = "Return of the King",
                    PublicationYear = 1954,
                    Description = "Concluding the story begun in The Hobbit, this is the final part of Tolkien’s epic masterpiece, The Lord of the Rings. The armies of the Dark Lord Sauron are massing as his evil shadow spreads ever wider. Men, Dwarves, Elves and Ents unite forces to do battle agains the Dark. Meanwhile, Frodo and Sam struggle further into Mordor in their heroic quest to destroy the One Ring. The devastating conclusion of J.R.R. Tolkien’s classic tale of magic and adventure, begun in The Fellowship of the Ring and The Two Towers.",
                    Pages = 624,
                    BookAuthor = new Author { ID = "1", FirstName = "J.R.R", LastName = "Tolkien", BirthYear = 1892},
                    BookGenre = new Genre { Name = "Fantasy", Art = null } },
                new Book {
                    ISBN = "978-0273775386",
                    Title = "Data Structures and Algorithm Analysis in C++",
                    PublicationYear = 2014,
                    Description = "Data Structures and Algorithm Analysis in C++ is an advanced algorithms book that bridges the gap between traditional CS2 and Algorithms Analysis courses. As the speed and power of computers increases, so does the need for effective programming and algorithm analysis.By approaching these skills in tandem, Mark Allen Weiss teaches readers to develop well - constructed, maximally efficient programs using the C++programming language. This book explains topics from binary heaps to sorting to NP - completeness, and dedicates a full chapter to amortized analysis and advanced data structures and their implementation.Figures and examples illustrating successive stages of algorithms contribute to Weiss’ careful, rigorous and in-depth analysis of each type of algorithm.",
                    Pages = 656,
                    BookAuthor = new Author { ID = "2", FirstName = "Mark A.", LastName = "Weiss", BirthYear = 1963},
                    BookGenre = new Genre { Name = "Textbook", Art = null } },
                new Book {
                    ISBN = "978-0007527502",
                    Title = "Murder on the Orient Express",
                    PublicationYear = 1934,
                    Description = "Agatha Christie’s most famous murder mystery. Just after midnight, a snowdrift stops the Orient Express in its tracks. The luxurious train is surprisingly full for the time of the year, but by the morning it is one passenger fewer.An American tycoon lies dead in his compartment, stabbed a dozen times, his door locked from the inside. Isolated and with a killer in their midst, detective Hercule Poirot must identify the murderer – in case he or she decides to strike again.",
                    Pages = 288,
                    BookAuthor = new Author { ID = "3", FirstName = "Agatha", LastName = "Christie", BirthYear = 1890},
                    BookGenre = new Genre { Name = "Thriller", Art = null } },
                new Book {
                    ISBN = "978-0552161275",
                    Title = "The Da Vinci Code",
                    PublicationYear = 2009,
                    Description = "The plot in ‘The Da Vinci Code’ revolves around Robert Langdon, who interprets symbols at Harvard. He gets a shocking phone call at mid-night while in Paris. The administrator of the Louvre was killed in the premises of the museum and the dead body is accompanied by a sequence of codes. As Robert was supposed to meet that person, he now stands as a suspect of the murder. Robert is accompanied by Sophie Neveu, a French cryptologist, in solving the mystery revolving around the murder. They are astonished to find that the hints they are searching for are hidden in the works of Leonardo Da Vinci.Although the clues can be seen clearly, yet they are to be decoded.As the story unfolds, it is found out that the late administrator was affiliated with some secret society and his sole purpose was to safeguard the secret.Robert and Sophie then battle to decode the secrets running from cathedrals to castles around the whole Europe. They are also being hunted by some anonymous antagonists.They duo needs to find out the reason for which the administrator sacrificed his life.Also, they have to maintain and protect the secret society’s mission which is being carried on for so many years.This write - up includes number of twists and turns that are capable enough to send thrill waves into the readers.",
                    Pages = 592,
                    BookAuthor = new Author { ID = "4", FirstName = "Dan", LastName = "Brown", BirthYear = 1964},
                    BookGenre = new Genre { Name = "Thriller", Art = null } },
                new Book {
                    ISBN = "978-0141322919",
                    Title = "Just Listen",
                    PublicationYear = 2007,
                    Description = "Just Listen is a captivating young adult novel about learning to forgive and forget from New York Times Number One bestseller Sarah Dessen, author of The Truth About Forever and Lock and Key, Sarah Dessen. I'm Annabel. I'm the girl who has it all. Model looks, confidence. A great social life. I'm one of the lucky ones. Aren't I? My 'best friend' is spreading rumours about me. My family is slowly falling apart. It's turning into a long, lonely summer, full of secrets and silence. But I've met this guy who won't let me hide away. He's one of those intense types, obsessed with music. He's determined to make me listen. And he's determined to make me smile. But can he help me forget what happened the night everything changed?",
                    Pages = 400,
                    BookAuthor = new Author { ID = "5", FirstName = "Sarah", LastName = "Dessen", BirthYear = 1970},
                    BookGenre = new Genre { Name = "Romance", Art = null } },
                new Book {
                    ISBN = "978-0345391803",
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    PublicationYear = 1995,
                    Description = "Seconds before the Earth is demolished to make way for a galactic freeway, Arthur Dent is plucked off the planet by his friend Ford Prefect, a researcher for the revised edition of The Hitchhiker’s Guide to the Galaxy who, for the last fifteen years, has been posing as an out-of-work actor. Together this dynamic pair begin a journey through space aided by quotes from The Hitchhiker’s Guide (“A towel is about the most massively useful thing an interstellar hitchhiker can have”) and a galaxy-full of fellow travelers: Zaphod Beeblebrox—the two-headed, three-armed ex-hippie and totally out-to-lunch president of the galaxy; Trillian, Zaphod’s girlfriend (formally Tricia McMillan), whom Arthur tried to pick up at a cocktail party once upon a time zone; Marvin, a paranoid, brilliant, and chronically depressed robot; Veet Voojagig, a former graduate student who is obsessed with the disappearance of all the ballpoint pens he bought over the years. Where are these pens? Why are we born? Why do we die? Why do we spend so much time between wearing digital watches? For all the answers stick your thumb to the stars. And don't forget to bring a towel!",
                    Pages = 224,
                    BookAuthor = new Author { ID = "6", FirstName = "Douglas", LastName = "Adams", BirthYear = 1952},
                    BookGenre = new Genre { Name = "Science Fiction", Art = null } },
                new Book {
                    ISBN = "978-0753555637",
                    Title = "Elon Musk: How the Billionaire CEO of Spacex and Tesla is Shaping Our Future",
                    PublicationYear = 2015,
                    Description = "The book captures the life and achievements of South African interpreter and innovator, Elon Musk, the brain behind series of successful enterprises such as PayPal, Tesla, SpaceX and Solarcity. The real-life inspiration of the Iron Man Series, Musk wants to be the saviour of the planet, send people into space and set up a colony on Mars. Bullied in school and scolded tremendously by his father, Musk was actually a brilliant student and his life story is nothing less than a drama packed film. Ashlee Vance’s brilliant description of Musk's character, simple language and neat choice of words indeed makes this book a great read. Considered by some as the innovation, entrepreneurial Steve Jobs of the present and future, Elon Musk became a billionaire early in life with his successful online ventures. One of the successful companies that he co-founded was the online payment gateway PayPal that was later acquired by e-Bay in 2002. Getting sacked as the CEO, Musk did not cease to amaze friend and foes alike with his out of the box ideas, like investing in rockets! Needless to say, this deconstructed obsession with technology had his marital life go haywire. The book 'Elon Musk: How the Billionaire CEO of SpaceX and TESLA is Shaping Our Future’ is a brilliant and intelligent account of this genius young 'iron man’ told in a gripping manner. Available in paperback from Penguin Random House publication, the book was published in 2015.",
                    Pages = 352,
                    BookAuthor = new Author { ID = "7", FirstName = "Ashlee", LastName = "Vance", BirthYear = 1977},
                    BookGenre = new Genre { Name = "Biography", Art = null } },
                new Book {
                    ISBN = "978-9383656950",
                    Title = "Calculus of One Variable",
                    PublicationYear = 2015,
                    Description = "The book is meant for a one-semester introductory course on Calculus of One Variable at the Bachelors levels of Science and Engineering programs. It provides clear understanding of the basic concepts of differential and integral calculus, and also introduces slightly advanced topics such as power series and Fourier series. The introduction of sequences and series as the first chapter of the book helps a great deal in the discussion of various other concepts in the later chapters. The student friendly approach of the exposition of the book would definitely be of great use not only for students, but also for the teachers of the course.",
                    Pages = 320,
                    BookAuthor = new Author { ID = "8", FirstName = "M. Thamban", LastName = "Nair", BirthYear = 1957},
                    BookGenre = new Genre { Name = "Textbook", Art = null } },
                new Book {
                    ISBN = "978-8192910901",
                    Title = "1984",
                    PublicationYear = 1949,
                    Description = "1984: A Novel, unleashes a unique plot as per which No One is Safe or Free. No place is safe to run or even hide from a dominating party leader, Big Brother, who is considered equal to God. This is a situation where everything is owned by the State. The world was seeing the ruins of World War II. Leaders such as Hitler, Stalin and Mussolini prevailed during this phase. Big Brother is always watching your actions. He even controls everyone’s feelings of love, to live and to discover. The basic plot of this historic novel revolves around the concept that no person has freedom to live life on his or her own terms. The present day is 1984. The whole world is gradually changing. The nations which enjoy freedom, have distorted into unpleasant and degraded places, in turn creating a powerful cartel known as Oceania. This is the world where the Big Brother controls everything. There is another character Winston Smith, who is leading a normal layman life under these harsh circumstances, though hating all of this. He works on writing the old newspaper articles in order to make history or past relevant to today’s party line. He is efficient enough in spite of hating his bosses. Julia, a young girl who is morally very rigid comes into the fore. She too hates the system as much as Winston does. Gradually, they get into an affair but have to conceal their feelings for each other, as it will not be acceptable by Big Brother.In Big Brother’s bad world, freedom is slavery and ignorance is strength.",
                    Pages = 310,
                    BookAuthor = new Author { ID = "9", FirstName = "George", LastName = "Orwell", BirthYear = 1903},
                    BookGenre = new Genre { Name = "Classic Fiction", Art = null } },
                new Book {
                    ISBN = "978-0099908401",
                    Title = "The Old Man and the Sea",
                    PublicationYear = 1952,
                    Description = "The last novel Ernest Hemingway saw published, The Old Man and the Sea has proved itself to be one of the enduring works of American fiction. It is the story of an old Cuban fisherman and his supreme ordeal: a relentless, agonizing battle with a giant marlin far out in the Gulf Stream. Using the simple. powerful language of a fable, Hemingway takes the timeless themes of courage in the face of defeat and personal triumph won from loss and transforms them into a magnificnet twentieth-century classic.",
                    Pages = 112,
                    BookAuthor = new Author { ID = "10", FirstName = "Ernest", LastName = "Hemingway", BirthYear = 1899},
                    BookGenre = new Genre { Name = "Classic Fiction", Art = null } },
            };

            authorList = new List<Author>
            {
                new Author { ID = "9", FirstName = "George", LastName = "Orwell", BirthYear = 1903 },
                new Author { ID = "8", FirstName = "M. Thamban", LastName = "Nair", BirthYear = 1957},
                new Author { ID = "6", FirstName = "Douglas", LastName = "Adams", BirthYear = 1952},
                new Author { ID = "4", FirstName = "Dan", LastName = "Brown", BirthYear = 1964},
            };

            adminList = new List<Admin>
            {
                new Admin { Username = "_destroyer2000", Password = "iDestroy" },
                new Admin { Username = "filip_A97", Password = "filipsPASS" },
                new Admin { Username = "flyckt89", Password = "DIF" },
                new Admin { Username = "Bengan", Password = "IamBengt" }
            };

        }

        public List<Author> AuthorList { get { return authorList; } set { AuthorList = value; } }
        public List<Book> BookList { get { return bookList; } set { bookList = value; } }

    }
}
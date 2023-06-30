using System;
using System.Data.Entity;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {

            //var context=new PlutoContext();
            //var query=
            //    from c in context.Courses
            //    where c.Name.Contains("c#")
            //    orderby c.Name
            //    select c;
            ////foreach (var c in query)
            ////{
            ////    Console.WriteLine(c.Name);
            ////}

            //var courses=context.Courses.Where(c=>c.Name.Contains("c#")).OrderBy(c=>c.Name);

            //foreach (var item in courses)
            //{
            //    Console.WriteLine(item.Name);
            //}


            //var context=new PlutoContext();
            //var queru=
            //  from c in context.Courses
            //  group c by c.Level into g 
            //  select g;

            //foreach ( var c in queru )
            //{
            //    Console.WriteLine("{0} ({1})",c.Key,c.Count());
            //}

            //var context = new PlutoContext();
            //var queru =
            //   from c in context.Courses
            //   join a in context.Authors on c.AuthorId equals a.Id
            //   select new { CourseName = c.Name, AuthorName = a.Name };


            //var context = new PlutoContext();
            //var queru =
            //    from a in context.Authors
            //    join c in context.Courses on a.Id equals c.AuthorId into g
            //    select new { AuthorName = a.Name, Courses = g.Count() };
            //foreach (var c in queru)
            //{
            //    Console.WriteLine("{0} ({1})",c.AuthorName,c.Courses);
            //}



            //var context = new PlutoContext();
            //var queru =
            //    from a in context.Authors
            //    from c in context.Courses
            //    select new { AuthorName = a.Name,CourseName=c.Name };
            //    foreach (var c in queru)
            //    {
            //        Console.WriteLine("{0}-{1}",c.AuthorName,c.CourseName);
            //    }

            // var context = new PlutoContext();
            //var tags= context.Courses
            //     .Where(c => c.Level == 1)
            //     .OrderByDescending(c => c.Name)
            //     .ThenByDescending(c => c.Level)
            //     .Select(c =>c.Tags)
            //     .Distinct();

            // foreach (var c in tags)
            // {
            //     foreach (var tag in c)
            //     {
            //         Console.WriteLine(tag.Name);

            //     }
            // }


            //var context = new PlutoContext();
            //var tags = context.Courses.GroupBy(c=>c.Level)


            //foreach (var c in tags)
            //{
            //    Console.WriteLine("Key" +c.Key);
            //    foreach (var tag in c)
            //    {
            //        Console.WriteLine("\t"+tag.Name);
            //    }
            //}


            //var context = new PlutoContext();
            //context.Courses.Join(context.Authors,
            //   c=>c.AuthorId,
            //   a=>a.Id,
            //    (course, author) => new
            //    {
            //        CourseName= course.Name,
            //        AuthorName= author.Name,
            //    }
            //    );

            // Partitioning

            // var context = new PlutoContext();
            //var courses=context.Courses.Skip(10).Take(10);

            //Element Operators

            //var context = new PlutoContext();
            //context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 100);
            //context.Courses.SingleOrDefault(c=>c.Id==1)

            //Quantifying

            //var context = new PlutoContext();
            //var allAbovel0Dollars=context.Courses.All(c => c.FullPrice > 10);
            //context.Courses.Any(c => c.Level == 1);



            //var context = new PlutoContext();
            //var courses = context.Courses.ToList().Where(c => c.IsBeginnerCourse == true);


            //foreach (var c in courses)
            //{
            //    Console.WriteLine(c.Name);
            //}

            //var context = new PlutoContext();
            //IEquatable<Course> courses = context.Courses;
            //var filtered=courses.Where(c=>c.Level == 1);
            //foreach (var course in filtered)
            //{
            //    Console.WriteLine(course.Name);
            //}

            //var context = new PlutoContext();
            //var course=context.Courses.Single(c=>c.Id==2);
            //foreach (var tag in course.Tags)
            //{
            //    Console.WriteLine(tag.Name);
            //}

            //var context = new PlutoContext();
            //var courses=context.Courses.Include(c=>c.Instructor).ToList();
            //foreach (var course in courses)
            //{
            //    Console.WriteLine("{0} by {1}",course.Name,course.Instructor.Name);
            //}

            //var author = new Author() { Id=1,Name="Mosh Hamedeni"};
            //var context = new PlutoContext();
            //var course = new Course()
            //{
            //    Name = "New Course",
            //    Description = "New Descriptio",
            //    FullPrice = 19.95f,
            //    Level = 1,
            //    Author = author
            //};S
            //context.Courses.Add(course);
            //context.SaveChanges();

            //var context = new PlutoContext();
            //var author=context.Authors.Include(a=>a.Courses).Single(a=>a.Id==2);
            //context.Courses.RemoveRange(author.Courses);
            //context.Authors.Remove(author);
            //context.SaveChanges();

            //var context = new PlutoContext();
            ////Add an object
            //context.Authors.Add(new Author { Name = "New Author" });
            ////Update an object
            //var author =context.Authors.First(3);
            //author.Name = "Updated";
            ////Remove an object
            //var another =context.Authors.First(4);
            //context.Authors.Remove(author);
            //context.ChangeTracker.Entries();
            //foreach (var entry in entries)
            //{
            //    entry.Reload();
            //    Console.WriteLine(entry.S);
            //}

        }

    }
}

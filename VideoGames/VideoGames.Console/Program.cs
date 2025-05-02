using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamerService = new CrudService<Gamer>(gamer => gamer.Id);

            var gamer1 = new Gamer {Id = Guid.NewGuid(), Username = "Nomad", Age = 26};
            var gamer2 = new Gamer {Id = Guid.NewGuid(), Username = "Psycho", Age = 39};
            
            //створення
            gamerService.Create(gamer1);
            gamerService.Create(gamer2);

            //читання
            Console.WriteLine("Геймери:")
            foreach (var gamer in gamerService.ReadAll())
            {
                Console.WriteLine($"{gamer.Username}, вік: {gamer.Age}");
            }

            //оновлення
            gamer1.Age = 27;
            gamerService.Update(gamer1);

            //видалення
            gamerService.Remove(gamer2);
        }
    }
}

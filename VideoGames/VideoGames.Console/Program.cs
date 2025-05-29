using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.Common;

namespace VideoGames.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamerService = new CrudService<Gamer>(gamer => gamer.Id);

            var gamer1 = new Gamer {Id = Guid.NewGuid(), Username = "Nomad", Age = 26};
            var gamer2 = new Gamer {Id = Guid.NewGuid(), Username = "Psycho", Age = 39};
            //шлях до файлу для збереження
            string filePath = "gamers.json";

            //створення
            gamerService.Create(gamer1);
            gamerService.Create(gamer2);

            //читання
            System.Console.WriteLine("Gamer list:");
            foreach (var gamer in gamerService.ReadAll())
            {
                System.Console.WriteLine($"id: {gamer.Id}, username: {gamer.Username}, age: {gamer.Age}");
            }
            // використання методу Save
            gamerService.Save(filePath);
            System.Console.WriteLine($"\nData saved in: {filePath}\n");

            //оновлення
            gamer1.Age = 27;
            gamerService.Update(gamer1);

            //видалення
            gamerService.Remove(gamer2);

            System.Console.WriteLine("Gamer list:");
            foreach (var gamer in gamerService.ReadAll())
            {
                System.Console.WriteLine($"id: {gamer.Id}, username: {gamer.Username}, age: {gamer.Age}");
            }

            // завантаженням даних з файлу методом Load
            gamerService.Load(filePath);
            System.Console.WriteLine($"\nData loaded from: {filePath}\n");

            System.Console.WriteLine("Gamer list:");
            foreach (var gamer in gamerService.ReadAll())
            {
                System.Console.WriteLine($"id: {gamer.Id}, username: {gamer.Username}, age: {gamer.Age}");
            }
        }
    }
}

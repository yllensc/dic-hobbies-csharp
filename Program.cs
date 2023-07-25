//Crea un programa en C# que permita registrar información de usuarios de una página de hobbies. El programa debe utilizar un diccionario donde la clave sea el número de identificación del usuario (que debe ser único) y el valor sea un objeto que contenga el nombre del usuario, su edad y una lista de hobbies a los que está inscrito.

//El programa debe proporcionar un menú con las siguientes opciones:
//
//1. Agregar usuario: Permite agregar un nuevo usuario al diccionario, ingresando su número de identificación, nombre, edad y los hobbies de preferencia.
//2. Mostrar usuario: Muestra la información de un usuario específico, ingresando su número de identificación.
//3. Mostrar todos los usuarios: Muestra la información de todos los usuarios registrados en el diccionario.
//4. Eliminar usuario: Permite eliminar un usuario del diccionario, ingresando su número de identificación.
//5. Salir: Termina el programa.

using System.Collections;
using System.Globalization;

Dictionary<string, User> Users = new();

int menuOption;
do
{
    menuOption = showMainMenu();
    if ((Menu)menuOption == Menu.Add)
    {
        AddUser();
    }
    else if ((Menu)menuOption == Menu.ListUser)
    {
        ShowUser();
    }
    else if ((Menu)menuOption == Menu.ListAll)
    {
        ShowAllUsers();
    }
    else if ((Menu)menuOption == Menu.DeleteUser)
    {
        DeleteUser();
    }
    else if ((Menu)menuOption == Menu.Exit)
    {
        Console.WriteLine("Bye,bye");
    }
    else
    {
        Console.WriteLine("No tenemos esta opción, revisa el menú");
    }

} while ((Menu)menuOption != Menu.Exit);

int showMainMenu()
{
    Console.WriteLine("\n-----------USERS-HOBBIES------------");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Ingrese la opción a realizar: ");
    Console.WriteLine("1. Agregar usuario");
    Console.WriteLine("2. Buscar usuario");
    Console.WriteLine("3. Listar todos los usuarios");
    Console.WriteLine("4. Eliminar usuario");
    Console.WriteLine("5. Salir");
    Console.ResetColor();

    // Read option
    string? optionMenu = Console.ReadLine();
    return Convert.ToInt32(optionMenu);
}

void AddUser()
{

    try
    {
        Console.Clear();
        Console.WriteLine("Número de identificación: ");
        long numIdentification = Int64.Parse(Console.ReadLine());
        bool verificationDoc = true;
        foreach (var document in Users.Keys)
        {
            if (document == numIdentification.ToString())
            {
                verificationDoc = false;
                break;
            }
        }
        if (verificationDoc)
        {
            Console.WriteLine("Nombre del usurio: ");
            string? nameUser = Console.ReadLine();

            Console.WriteLine("¿Cuál es tu edad?");
            int ageUser = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("¿Cuántos hobbies tienes?");
            int numHobbies = Convert.ToInt32(Console.ReadLine());

            if (!string.IsNullOrEmpty(nameUser) && numIdentification > 0 && ageUser > 0 && numHobbies >= 0)
            {
                User user = new()
                {
                    Name = nameUser,
                    Age = ageUser,
                    Hobbies = AddHobbies(numHobbies),
                };
                Users.Add(numIdentification.ToString(), user);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contacto registrado correctamente");
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Los datos del contacto no están completos.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El documento ya está asociado a otro usuario.");
            Console.ResetColor();
        }

    }
    catch (Exception)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ha ocurrido un error al intentar ingresar el contacto, ups.");
        Console.ResetColor();
    }
}

List<string> AddHobbies(int numHobbies)
{
    List<string> list = new();
    int countHobbies = 0;
    do
    {
        ++countHobbies;
        Console.WriteLine($"Hobbie {countHobbies}");
        string? hobbieToAdd = Console.ReadLine();
        if (!string.IsNullOrEmpty(hobbieToAdd))
        {
            list.Add(hobbieToAdd);
        }

    } while (countHobbies < numHobbies);
    return list;
}

void ShowUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa el número de documento del usuario a consultar: ");
    long numIdentificationLook = Int64.Parse(Console.ReadLine());
    foreach (var document in Users.Keys)
    {
        if (document == numIdentificationLook.ToString())
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nDoc I.\tNombre\tEdad\tHobbies");
            Console.ResetColor();
            Console.WriteLine($"{document}\t{Users[document].Name}\t{Users[document].Age}\t{ListHobbies(Users[document].Hobbies)}");
            break;
        }
    }
}

void ShowAllUsers()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\nDoc I.\tNombre\tEdad\tHobbies");
    Console.ResetColor();
    foreach (var document in Users.Keys)
    {
        Console.WriteLine($"{document}\t{Users[document].Name}\t{Users[document].Age}\t{ListHobbies(Users[document].Hobbies)}");
    }
}

void DeleteUser(){
    Console.Clear();
    ShowAllUsers();
    Console.WriteLine("Ingresa el número de documento del usuario a eliminar: ");
    long numIdentificationDel = Int64.Parse(Console.ReadLine());
    foreach (var document in Users.Keys)
    {
        if (document == numIdentificationDel.ToString())
        {
            Users.Remove(document);
            break;
        }
    }
}

string ListHobbies(List<string> Hobbies)
{
    string strHobbies = "";
    int indexHobbie = 0;
    foreach (var hobbie in Hobbies)
    {
        strHobbies = strHobbies + " " + hobbie;
        indexHobbie++;
    }
    return strHobbies;

}

public enum Menu
{
    Add = 1,
    ListUser = 2,
    ListAll = 3,
    DeleteUser = 4,
    Exit = 5,
}

public class User
{
    public string? Name;
    public int Age;
    public List<string> Hobbies = new();
}
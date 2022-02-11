using CarMaintenance.Business;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                const string baseUrl = "https://localhost:7017/api/";
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var flag = true;
                while (flag)
                {
                    Console.WriteLine("Press 1 for car details");
                    Console.WriteLine("Press 2 for shop services");
                    Console.WriteLine("Press 3 for car in maintenance");
                    Console.WriteLine("Press 4 to add car in maintenance");
                    Console.WriteLine("Press 5 to exit");
                    var input = Console.ReadLine();
                    if (input == null)
                        continue;
                    int choice = int.Parse(input);
                    switch (choice)
                    {
                        case 1:
                            {
                                var registrationNumbers = GetCars(client);
                                var registartionNumber = Console.ReadLine();
                                if (string.IsNullOrEmpty(registartionNumber))
                                    continue;

                                if (registartionNumber != null && !registrationNumbers.Contains(registartionNumber))
                                {
                                    Console.WriteLine("Invalid RegistartionNumber");
                                    continue;
                                }
                                var cardetails = client.GetAsync($"CarMainentance/CarDetails/{registartionNumber}").Result;
                                if (cardetails.IsSuccessStatusCode)
                                {
                                    var car = cardetails.Content.ReadAsStringAsync().Result;
                                    var carModel = JsonConvert.DeserializeObject<CarModel>(car);
                                    if (carModel != null)
                                    {
                                        Console.WriteLine("OwnerName = " + carModel.OwnerName);
                                        Console.WriteLine("RegistrationNumber = " + carModel.RegistrationNumber);
                                        Console.WriteLine("Model = " + carModel.Model);
                                        Console.WriteLine("Make = " + carModel.Make);
                                    }

                                }
                            }
                            break;
                        case 2:
                            {
                                var services = GetShopServices(client);
                                foreach (var item in services)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        case 3:
                            {
                                var maintenaceRequets = client.GetAsync("CarMainentance/GetMaintenanceRequests").Result;
                                if (maintenaceRequets.IsSuccessStatusCode)
                                {
                                    var json = maintenaceRequets.Content.ReadAsStringAsync().Result;
                                    var maintenanceRequestModels = JsonConvert.DeserializeObject<List<MaintenanceRequestModel>>(json);

                                    if (maintenanceRequestModels == null)
                                    {
                                        Console.WriteLine("Something went wrong");
                                        continue;
                                    }
                                    if (maintenanceRequestModels != null && !maintenanceRequestModels.Any())
                                    {
                                        Console.WriteLine("No car added");
                                        continue;
                                    }
                                    foreach (var item in maintenanceRequestModels ?? new List<MaintenanceRequestModel>())
                                    {
                                        Console.WriteLine("status=" + item.Status);
                                        Console.WriteLine("selected services=" + string.Join(",", item.SelectedServices));
                                        Console.WriteLine("OwnerName = " + item.Car.OwnerName);
                                        Console.WriteLine("RegistrationNumber = " + item.Car.RegistrationNumber);
                                        Console.WriteLine("Model = " + item.Car.Model);
                                        Console.WriteLine("Make = " + item.Car.Make);
                                    }
                                }
                            }
                            break;
                        case 4:
                            {
                                var registrationNumbers = GetCars(client);
                                var registartionNumber = Console.ReadLine();
                                if (string.IsNullOrEmpty(registartionNumber))
                                    continue;

                                if (registartionNumber != null && !registrationNumbers.Contains(registartionNumber))
                                {
                                    Console.WriteLine("Invalid RegistartionNumber");
                                    continue;
                                }
                                Console.WriteLine("how many services you want?1-5");
                                var services = GetShopServices(client);
                                int j = 0;
                                foreach (var item in services)
                                {
                                    Console.WriteLine($"press {j++} for={item}");
                                }
                                var inputService = Console.ReadLine();
                                if (string.IsNullOrEmpty(inputService))
                                    continue;
                                var count = int.Parse(inputService);
                                if (count > 5)
                                {
                                    Console.WriteLine("Invalid Input");
                                    continue;
                                }

                                var tempservices = new List<ShopServices>();
                                for (int i = 0; i < count; i++)
                                {
                                    var inputShopService = Console.ReadLine();
                                    if (int.TryParse(inputShopService, out int service))
                                    {
                                        if (Enum.IsDefined(typeof(ShopServices), service))
                                        {
                                            ShopServices shopService = (ShopServices)service;
                                            tempservices.Add(shopService);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid shopService");
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input");
                                        continue;
                                    }
                                }

                                if (tempservices.Any())
                                {
                                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(tempservices), Encoding.UTF8);
                                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                    var cardetails = client.PostAsync($"CarMainentance/CarsInMaintenance?registrationNumber={registartionNumber}", httpContent).Result;
                                    if (cardetails.IsSuccessStatusCode)
                                    {
                                        var car = cardetails.Content.ReadAsStringAsync().Result;
                                        var isAdded = JsonConvert.DeserializeObject<bool>(car);
                                        if (isAdded)
                                        {
                                            Console.WriteLine("Car is added to maintenance");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Car is not added to maintenance");
                                        }
                                    }
                                    else if (cardetails.StatusCode == System.Net.HttpStatusCode.BadRequest)
                                    {
                                        var errorMessage = cardetails.Content.ReadAsStringAsync().Result;
                                        Console.WriteLine(errorMessage);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input");
                                    continue;
                                }
                            }
                            break;
                        case 5: flag = false; break;
                        default:
                            Console.WriteLine("Unknown choice.");
                            break;
                    }

                }
            }

        }

        private static List<string> GetShopServices(HttpClient client)
        {
            var shopServiceMessage = client.GetAsync("CarMainentance/ShopServices").Result;
            if (shopServiceMessage.IsSuccessStatusCode)
            {
                var json = shopServiceMessage.Content.ReadAsStringAsync().Result;
                var services = JsonConvert.DeserializeObject<List<string>>(json);
                if (services != null)
                    return services;
            }
            return new List<string>();
        }

        private static List<string> GetCars(HttpClient client)
        {
            var cars = client.GetAsync("CarMainentance/Cars").Result;
            if (cars.IsSuccessStatusCode)
            {
                var json = cars.Content.ReadAsStringAsync().Result;
                var registartionNumbers = JsonConvert.DeserializeObject<List<string>>(json);
                Console.WriteLine("Select any below Registration Number");
                if (registartionNumbers != null)
                {
                    registartionNumbers.ForEach(e =>
                    {
                        Console.WriteLine(e);
                    });
                    return registartionNumbers;
                }
            }
            return new List<string>();
        }
    }
}
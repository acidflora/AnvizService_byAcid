using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using Anviz.SDK;

namespace Acid.AnvizService
{
    static class controller
    {

        private static readonly int[] PORTS = { 5010, 5050 };
        private const ulong DEVICE_ID = 1;
        private const string DEVICE_HOST = "10.0.0.1";
        public static async Task writePerson(ulong idPerson, string name, ulong password)
        {

            var manager = new AnvizManager();
            manager.Listen();
            Console.WriteLine($"Listening on port 5010");
            using (var device = await manager.Accept())
            {
                int a = 0;

                device.DevicePing += (s, e) => Console.WriteLine("Device Ping Received");
                device.ReceivedPacket += (s, e) => Console.WriteLine("Received packet");
                device.DeviceError += (s, e) => throw e;

                var id = device.DeviceId;
                var sn = await device.GetDeviceSN();
                var type = await device.GetDeviceTypeCode();
                var biotype = await device.GetDeviceBiometricType();

                Console.WriteLine($"Connected to device {type} ID {id} SN {sn} BIO {biotype}");

                var employee = new Anviz.SDK.Responses.UserInfo(idPerson, name);
                employee.Password = password;
                await device.SetEmployeesData(employee);
                Console.WriteLine("Created user, begin fp enroll");
                var fp = await device.EnrollFingerprint(employee.Id);
                await device.SetFingerprintTemplate(employee.Id, Anviz.SDK.Utils.Finger.RightIndex, fp);

            }
            Console.ReadLine();
        }
        public static async Task<List<Anviz.SDK.Responses.Record>> getRecs(int port)
        {
            var manager = new AnvizManager();

            manager.Listen(port);

            Console.WriteLine($"Listening on port {port}");
            using (var device = await manager.Accept())
            {
                device.DevicePing += (s, e) => Console.WriteLine("Device Ping Received");
                device.ReceivedPacket += (s, e) => Console.WriteLine("Received packet");
                device.DeviceError += (s, e) => throw e;

                var id = device.DeviceId;
                var sn = await device.GetDeviceSN();
                var type = await device.GetDeviceTypeCode();
                var biotype = await device.GetDeviceBiometricType();

                Console.WriteLine($"Connected to device {type} ID {id} SN {sn} BIO {biotype}");

                if (id != DEVICE_ID)
                {
                    await device.SetDeviceID(DEVICE_ID);
                }

                var stats = await device.GetDownloadInformation();
                Console.WriteLine($"TotalUsers {stats.UserAmount} TotalRecords {stats.AllRecordAmount}");
                var employees = await device.GetEmployeesData();

                var dict = new Dictionary<ulong, string>();
                foreach (var employee in employees)
                {
                    dict.Add(employee.Id, employee.Name);
                    Console.WriteLine($"Employee {employee.Id} -> {employee.Name} pwd {employee.Password} card {employee.Card} fp {string.Join(", ", employee.EnrolledFingerprints)}");
                    foreach (var f in employee.EnrolledFingerprints)
                    {
                        var fp = await device.GetFingerprintTemplate(employee.Id, f);
                        Console.WriteLine($"-> {f} {Convert.ToBase64String(fp)}");
                    }
                    await device.SetRecords(new Anviz.SDK.Responses.Record(employee.Id));
                }


                List<Anviz.SDK.Responses.Record> records = await device.DownloadRecords(true); //true to get only new records
                foreach (var rec in records)
                {
                    Console.WriteLine($"Employee {rec.UserCode}:{dict[rec.UserCode]} at {rec.DateTime.ToLongDateString()} {rec.DateTime.ToLongTimeString()}");
                }
                await device.ClearNewRecords();
                return records;

            }

            Console.ReadLine();
        }
    }
}

﻿using Anviz.SDK;
using System;
using System.Collections.Generic;
using System.Threading; 
using System.Threading.Tasks;

namespace Acid.AnvizService
{
    class Program
    {
        private static List<Anviz.SDK.Responses.Record> Recs { get; set; } = null;
        public static void Main()
        {

        }
        


        public static async Task<List<Anviz.SDK.Responses.Record>> Records(int port)
        {
            Recs = null;
            Recs.AddRange(await controller.getRecs(port));
            return Recs;
        }
//        public static async Task MainAsync()
//        {
//            var manager = new AnvizManager();
//#if false //authenticate with device password
//            manager.ConnectionUser = "0";
//            manager.ConnectionPassword = "12345";
//            manager.AuthenticateConnection = true;
//#endif
            
//#if true //false for client mode
//            manager.Listen();
//            Console.WriteLine($"Listening on port 5010");
//            using (var device = await manager.Accept())
//#else
//            using (var device = await manager.Connect(DEVICE_HOST))
//#endif
//            {
//                device.DevicePing += (s, e) => Console.WriteLine("Device Ping Received");
//                device.ReceivedPacket += (s, e) => Console.WriteLine("Received packet");
//                device.DeviceError += (s, e) => throw e;
//                var id = device.DeviceId;
//                var sn = await device.GetDeviceSN();
//                var type = await device.GetDeviceTypeCode();
//                var biotype = await device.GetDeviceBiometricType();
//                Console.WriteLine($"Connected to device {type} ID {id} SN {sn} BIO {biotype}");
//                if (id != DEVICE_ID)
//                {
//                    await device.SetDeviceID(DEVICE_ID);
//                }
//                var now = DateTime.Now;
//                var deviceTime = await device.GetDateTime();
//                Console.WriteLine($"Current device time is {deviceTime.ToShortDateString()} {deviceTime.ToShortTimeString()}");
//                if (Math.Abs((now - deviceTime).TotalSeconds) > 1)
//                {
//                    await device.SetDateTime(now);
//                    Console.WriteLine("Updated device time according to local time");
//                }
//                var net = await device.GetTcpParameters();
//                Console.WriteLine($"Device IP is {net.IP} {net.SubnetMask} {net.DefaultGateway} {net.MacAddress} mode is {net.TcpMode.ToString()}");
//#if false //here you can change network parameters
//                net.DefaultGateway = IPAddress.Parse("10.0.0.5");
//                await device.SetTCPParameters(net);
//#endif
//                var basic = await device.GetBasicSettings();
//                Console.WriteLine($"FW {basic.Firmware} AdminPWD {basic.ManagementPassword} Vol {basic.Volume} DateFormat {basic.DateFormat} 24h {basic.Is24HourClock}");
//#if false //here you can change basic parameters
//                basic.Volume = Anviz.SDK.Responses.Volume.Off;
//                basic.DateFormat = Anviz.SDK.Responses.DateFormat.DDMMYY;
//                basic.Is24HourClock = true;
//                await device.SetBasicSettings(basic);
//#endif
//                var advanced = await device.GetAdvancedSettings();
//                Console.WriteLine($"FPPrecision {advanced.FPPrecision} Delay {advanced.RepeatAttendanceDelay}");
//#if false //here you can change advanced parameters
//                advanced.FPPrecision = Anviz.SDK.Responses.FPPrecision.Medium;
//                advanced.RepeatAttendanceDelay = 1;
//                await device.SetAdvancedSettings(advanced);
//#endif
//                var stats = await device.GetDownloadInformation();
//                Console.WriteLine($"TotalUsers {stats.UserAmount} TotalRecords {stats.AllRecordAmount}");
//                var employees = await device.GetEmployeesData();
//                var dict = new Dictionary<ulong, string>();
//                foreach (var employee in employees)
//                {
//                    dict.Add(employee.Id, employee.Name);
//                    Console.WriteLine($"Employee {employee.Id} -> {employee.Name} pwd {employee.Password} card {employee.Card} fp {string.Join(", ", employee.EnrolledFingerprints)}");
//                    foreach (var f in employee.EnrolledFingerprints)
//                    {
//                        var fp = await device.GetFingerprintTemplate(employee.Id, f);
//                        Console.WriteLine($"-> {f} {Convert.ToBase64String(fp)}");
//                    }
//                    await device.SetRecords(new Anviz.SDK.Responses.Record(employee.Id));
//                }
//                if (!dict.ContainsValue("TEST"))  //добавление?!
//                {
//                    var employee = new Anviz.SDK.Responses.UserInfo(stats.UserAmount + 1, "TEST");
//                    await device.SetEmployeesData(employee);
//                    Console.WriteLine("Created test user, begin fp enroll");
//                    var fp = await device.EnrollFingerprint(employee.Id);
//                    await device.SetFingerprintTemplate(employee.Id, Anviz.SDK.Utils.Finger.RightIndex, fp);
//                }
//                var records = await device.DownloadRecords(true); //true to get only new records
//                foreach (var rec in records)
//                {
//                    Console.WriteLine($"Employee {dict[rec.UserCode]} at {rec.DateTime.ToLongDateString()} {rec.DateTime.ToLongTimeString()}");
//                }
//                await device.ClearNewRecords();
//            }
//            Console.ReadLine();
//        }
    }
    
}

﻿using FelicaLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace UnitTest.Scenarios
{
    /// <summary>
    /// Edy に接続できる場合のテストです。
    /// </summary>
    [TestClass]
    public class EdyTest
    {
        [TestMethod]
        public void Felica_TryConnectionToPort()
        {
            using (var felica = new Felica(FelicaSystemCode.Any))
            {
                Assert.AreEqual(true, felica.TryConnectionToPort());
            }
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                Assert.AreEqual(true, felica.TryConnectionToPort());
            }
            using (var felica = new Felica(FelicaSystemCode.Suica))
            {
                Assert.AreEqual(true, felica.TryConnectionToPort());
            }
        }

        [TestMethod]
        public void Felica_TryConnectionToCard()
        {
            using (var felica = new Felica(FelicaSystemCode.Any))
            {
                Assert.AreEqual(true, felica.TryConnectionToCard());
            }
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                Assert.AreEqual(true, felica.TryConnectionToCard());
            }
            using (var felica = new Felica(FelicaSystemCode.Suica))
            {
                Assert.AreEqual(false, felica.TryConnectionToCard());
            }
        }

        [TestMethod]
        public void Felica_GetIDm()
        {
            using (var felica = new Felica(FelicaSystemCode.Any))
            {
                Debug.WriteLine(felica.GetIDm().ToHexString());
            }
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                Debug.WriteLine(felica.GetIDm().ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_GetIDm_Suica()
        {
            using (var felica = new Felica(FelicaSystemCode.Suica))
            {
                Debug.WriteLine(felica.GetIDm().ToHexString());
            }
        }

        [TestMethod]
        public void Felica_GetPMm()
        {
            using (var felica = new Felica(FelicaSystemCode.Any))
            {
                Debug.WriteLine(felica.GetPMm().ToHexString());
            }
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                Debug.WriteLine(felica.GetPMm().ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_GetPMm_Suica()
        {
            using (var felica = new Felica(FelicaSystemCode.Suica))
            {
                Debug.WriteLine(felica.GetPMm().ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_ReadWithoutEncryption_Any()
        {
            using (var felica = new Felica(FelicaSystemCode.Any))
            {
                Debug.WriteLine(felica.ReadWithoutEncryption(FelicaServiceCode.EdyBalance, 0).ToHexString());
            }
        }

        [TestMethod]
        public void Felica_ReadWithoutEncryption_Edy()
        {
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                Debug.WriteLine(felica.ReadWithoutEncryption(FelicaServiceCode.EdyBalance, 0).ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_ReadWithoutEncryption_Edy_OutOfRange()
        {
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                Debug.WriteLine(felica.ReadWithoutEncryption(FelicaServiceCode.EdyBalance, 1).ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_ReadWithoutEncryption_Waon()
        {
            using (var felica = new Felica(FelicaSystemCode.Waon))
            {
                Debug.WriteLine(felica.ReadWithoutEncryption(FelicaServiceCode.WaonBalance, 0).ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_ReadWithoutEncryption_Suica()
        {
            using (var felica = new Felica(FelicaSystemCode.Suica))
            {
                Debug.WriteLine(felica.ReadWithoutEncryption(FelicaServiceCode.SuicaAttributes, 0).ToHexString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_ReadBlocksWithoutEncryption_Any()
        {
            using (var felica = new Felica(FelicaSystemCode.Any))
            {
                foreach (var data in felica.ReadBlocksWithoutEncryption(FelicaServiceCode.EdyHistory, 0, 6))
                {
                    Debug.WriteLine(data.ToHexString());
                }
            }
        }

        [TestMethod]
        public void Felica_ReadBlocksWithoutEncryption_Edy()
        {
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                foreach (var data in felica.ReadBlocksWithoutEncryption(FelicaServiceCode.EdyHistory, 0, 6))
                {
                    Debug.WriteLine(data.ToHexString());
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Felica_ReadBlocksWithoutEncryption_Edy_OutOfRange()
        {
            using (var felica = new Felica(FelicaSystemCode.Edy))
            {
                foreach (var data in felica.ReadBlocksWithoutEncryption(FelicaServiceCode.EdyHistory, 5, 2))
                {
                    Debug.WriteLine(data.ToHexString());
                }
            }
        }

        [TestMethod]
        public void FelicaUtility_TryConnectionToPort()
        {
            Assert.AreEqual(true, FelicaUtility.TryConnectionToPort());
        }

        [TestMethod]
        public void FelicaUtility_TryConnectionToCard()
        {
            Assert.AreEqual(true, FelicaUtility.TryConnectionToCard(FelicaSystemCode.Any));
            Assert.AreEqual(true, FelicaUtility.TryConnectionToCard(FelicaSystemCode.Edy));
            Assert.AreEqual(false, FelicaUtility.TryConnectionToCard(FelicaSystemCode.Suica));
        }

        [TestMethod]
        public void FelicaUtility_GetIDm()
        {
            Debug.WriteLine(FelicaUtility.GetIDm(FelicaSystemCode.Any).ToHexString());
            Debug.WriteLine(FelicaUtility.GetIDm(FelicaSystemCode.Edy).ToHexString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FelicaUtility_GetIDm_Suica()
        {
            Debug.WriteLine(FelicaUtility.GetIDm(FelicaSystemCode.Suica).ToHexString());
        }

        [TestMethod]
        public void FelicaUtility_GetPMm()
        {
            Debug.WriteLine(FelicaUtility.GetPMm(FelicaSystemCode.Any).ToHexString());
            Debug.WriteLine(FelicaUtility.GetPMm(FelicaSystemCode.Edy).ToHexString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FelicaUtility_GetPMm_Suica()
        {
            Debug.WriteLine(FelicaUtility.GetPMm(FelicaSystemCode.Suica).ToHexString());
        }

        [TestMethod]
        public void FelicaUtility_ReadWithoutEncryption_Edy()
        {
            Debug.WriteLine(FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Edy, FelicaServiceCode.EdyBalance, 0).ToHexString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FelicaUtility_ReadWithoutEncryption_Waon()
        {
            Debug.WriteLine(FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Waon, FelicaServiceCode.WaonBalance, 0).ToHexString());
        }

        [TestMethod]
        public void FelicaUtility_ReadBlocksWithoutEncryption_Edy()
        {
            foreach (var data in FelicaUtility.ReadBlocksWithoutEncryption(FelicaSystemCode.Edy, FelicaServiceCode.EdyHistory, 0, 6))
            {
                Debug.WriteLine(data.ToHexString());
            }
        }

        [TestMethod]
        public void FelicaHelper_GetEdyBalance()
        {
            Debug.WriteLine(FelicaHelper.GetEdyBalance());
        }

        [TestMethod]
        public void FelicaHelper_GetEdyHistory()
        {
            foreach (var item in FelicaHelper.GetEdyHistory())
            {
                Debug.WriteLine(string.Format("{0}, ID: {1}, 利用区分: {2}, 利用額: {3}, 残高: {4}", item.DateTime, item.TransactionId, item.UsageCode, item.Amount, item.Balance));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FelicaHelper_GetWaonBalance()
        {
            Debug.WriteLine(FelicaHelper.GetWaonBalance());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FelicaHelper_GetSuicaBalance()
        {
            Debug.WriteLine(FelicaHelper.GetSuicaBalance());
        }
    }
}

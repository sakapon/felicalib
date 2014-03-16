﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FelicaLib
{
    /// <summary>
    /// FeliCa のシステム コードを表します。
    /// </summary>
    public enum FelicaSystemCode
    {
        /// <summary>すべて。</summary>
        Any = 0xFFFF,
        /// <summary>共通領域。</summary>
        Common = 0xFE00,
        /// <summary>サイバネ領域。</summary>
        Cybernetics = 0x0003,

        /// <summary>Edy。共通領域を使用します。</summary>
        Edy = Common,
        /// <summary>WAON。共通領域を使用します。</summary>
        Waon = Common,
        /// <summary>Suica。サイバネ領域を使用します。</summary>
        Suica = Cybernetics,
        /// <summary>QUICPay。</summary>
        QuicPay = 0x04C1,
    }

    /// <summary>
    /// FeliCa に関するヘルパー メソッドを提供します。
    /// </summary>
    public static class FelicaHelper
    {
        /// <summary>
        /// Edy の残高を取得します。
        /// </summary>
        /// <returns>Edy の残高。</returns>
        public static int GetEdyBalance()
        {
            var data = FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Edy, 0x1317, 0);
            return data.ToEdyBalance();
        }

        /// <summary>
        /// WAON の残高を取得します。
        /// </summary>
        /// <returns>WAON の残高。</returns>
        public static int GetWaonBalance()
        {
            var data = FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Waon, 0x6817, 0);
            return data.ToWaonBalance();
        }

        /// <summary>
        /// Suica の残高を取得します。PASMO などの交通系 IC カードと互換性があります。
        /// </summary>
        /// <returns>Suica の残高。</returns>
        public static int GetSuicaBalance()
        {
            var data = FelicaUtility.ReadWithoutEncryption(FelicaSystemCode.Suica, 0x008B, 0);
            return data.ToSuicaBalance();
        }

        /// <summary>
        /// Edy の残高情報のバイナリ データを残高に変換します。
        /// </summary>
        /// <param name="data">バイナリ データ。</param>
        /// <returns>Edy の残高。</returns>
        public static int ToEdyBalance(this byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");

            return data
                .Take(4)
                .Select((b, i) => b * (int)Math.Pow(256, i))
                .Sum();
        }

        /// <summary>
        /// WAON の残高情報のバイナリ データを残高に変換します。
        /// </summary>
        /// <param name="data">バイナリ データ。</param>
        /// <returns>WAON の残高。</returns>
        public static int ToWaonBalance(this byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");

            return data
                .Take(2)
                .Select((b, i) => b * (int)Math.Pow(256, i))
                .Sum();
        }

        /// <summary>
        /// Suica の属性情報のバイナリ データを残高に変換します。PASMO などの交通系 IC カードと互換性があります。
        /// </summary>
        /// <param name="data">バイナリ データ。</param>
        /// <returns>Suica の残高。</returns>
        public static int ToSuicaBalance(this byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");

            return data
                .Skip(11)
                .Take(2)
                .Select((b, i) => b * (int)Math.Pow(256, i))
                .Sum();
        }

        /// <summary>
        /// バイト配列を 16 進数表記の文字列に変換します。
        /// </summary>
        /// <param name="data">バイト配列。</param>
        /// <param name="lowercase">アルファベットを小文字で表記する場合は <see langword="true"/>。</param>
        /// <returns>16 進数表記の文字列。</returns>
        public static string ToHexString(this byte[] data, bool lowercase = false)
        {
            if (data == null) throw new ArgumentNullException("data");

            var format = lowercase ? "x2" : "X2";
            return data
                .Select(b => b.ToString(format))
                .ConcatStrings();
        }

        static string ConcatStrings(this IEnumerable<string> source)
        {
            return string.Concat(source.ToArray());
        }
    }
}
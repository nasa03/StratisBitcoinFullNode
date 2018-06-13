﻿using System.Diagnostics;
using System.IO;

namespace Stratis.Bitcoin.IntegrationTests.Common.Runners
{
    public sealed class BitcoinCoreRunner : NodeRunner
    {
        private string bitcoinD;

        public BitcoinCoreRunner(string dataDir, string bitcoinD)
            : base(dataDir)
        {
            this.bitcoinD = bitcoinD;
        }

        private Process process;

        public new bool IsDisposed
        {
            get { return this.process == null && this.process.HasExited; }
        }

        public new void Kill()
        {
            if (!this.IsDisposed)
            {
                this.process.Kill();
                this.process.WaitForExit();
            }
        }

        public override void OnStart()
        {
            this.process = Process.Start(new FileInfo(this.bitcoinD).FullName, $"-conf=bitcoin.conf -datadir={this.DataFolder} -debug=net");
        }

        public override void BuildNode()
        {
        }
    }
}
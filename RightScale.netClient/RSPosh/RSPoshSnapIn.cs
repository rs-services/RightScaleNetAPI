using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace RSPosh
{
    [RunInstaller(true)]
    public class PowerShellCmdlet_SnapIn : CustomPSSnapIn
    {
        private Collection<CmdletConfigurationEntry> _cmdlets;

        /// <summary>
        /// Gets description of powershell snap-in.
        /// </summary>
        public override string Description
        {
            get { return "A Description of MyCmdlet"; }
        }

        /// <summary>
        /// Gets name of power shell snap-in
        /// </summary>
        public override string Name
        {
            get { return "RSPosh"; }
        }

        /// <summary>
        /// Gets name of the vendor
        /// </summary>
        public override string Vendor
        {
            get { return ""; }
        }

        public override Collection<CmdletConfigurationEntry> Cmdlets
        {
            get
            {
                if (null == _cmdlets)
                {
                    _cmdlets = new Collection<CmdletConfigurationEntry>();
                    _cmdlets.Add(new CmdletConfigurationEntry
                      ("Get-RSPoshCmdlet", typeof(RSPosh.server_index), "Get-MyCmdlet.dll-Help.xml"));
                }
                return _cmdlets;
            }
        }

    }
}

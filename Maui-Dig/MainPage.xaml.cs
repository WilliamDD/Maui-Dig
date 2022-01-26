using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DnsClient;
using DnsClient.Protocol;
using DnsClient.Protocol.Options;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace Maui_Dig
{
	public partial class MainPage : FlyoutPage
	{
		public MainPage()
		{
			InitializeComponent();

			
		}

		private async void OnCounterClicked(object sender, EventArgs e)
		{

			var dnsServer = new IPEndPoint(IPAddress.Parse("192.168.178.1"), 53);
			var queryClass = QueryClass.IN;
			var queryType = QueryType.ANY;
			var recursion = false;
			var resolverCache = false;
			var tcpOnly = false;
			var query = "google.com";
			var ConvertIpAddressToArpaWhenUsingPTR = true;

			var clientOptions = new LookupClientOptions(dnsServer);
			clientOptions.Recursion = recursion;
			clientOptions.UseCache = resolverCache;
			clientOptions.UseTcpOnly = tcpOnly;

			var lookupClient = new LookupClient(clientOptions);
			var result = await lookupClient.QueryAsync(query, queryType,queryClass);

			resultText.Text += result.Header;
			resultText.Text += Environment.NewLine;
			resultText.Text += Environment.NewLine;

			resultText.Text += ";; OPT PSEUDOSECTION:";
			foreach (OptRecord additional in result.Additionals)
			{
				resultText.Text += $"; EDNS: version: {additional.Version}, flags:; UDP: {additional.UdpSize}; code: {additional.ResponseCodeEx}; dnsSec: {additional.IsDnsSecOk}";
			}

			resultText.Text += Environment.NewLine;
			resultText.Text += Environment.NewLine;
			resultText.Text += ";; Question section";
			foreach (var question in result.Questions)
			{
				resultText.Text += question;
			}
			resultText.Text += Environment.NewLine;
			resultText.Text += Environment.NewLine;
			resultText.Text += ";; Answer section";
			foreach (var answer in result.Answers)
			{
				resultText.Text += answer;
			}

			//SemanticScreenReader.Announce(resultView.ItemsSource);
		}
	}
}

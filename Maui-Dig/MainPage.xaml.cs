using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DnsClient;
using DnsClient.Protocol;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace Maui_Dig
{
	public partial class MainPage : ContentPage
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
			var resultList = new List<Cell>();
			foreach (var cell in result.Answers.OfType<ARecord>())
            {
				resultList.Add(new TextCell() { Text = cell.DomainName, Detail = $"{cell.RecordClass} {cell.RecordType} {cell.TimeToLive} {cell.Address}" });
            }
			resultView.ItemsSource = resultList;

			//SemanticScreenReader.Announce(resultView.ItemsSource);
		}
	}
}

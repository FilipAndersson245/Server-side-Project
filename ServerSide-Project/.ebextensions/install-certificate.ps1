import-module webadministration
# Change to your password for the certificate.pfx file.
$password = "jffa8997"
$certfile = ".ebextensions\certificate.pfx"
mv $certfile "C:\certification.pfx"
# Cleanup existing binding.

if ( Get-WebBinding "Default Web Site" -Port 443 )
{
	Echo "Removing WebBinding"
	Remove-WebBinding -Name "Default Web Site" -BindingInformation *:443:
}

if ( Get-Item -path IIS:\SslBindings\0.0.0.0!443 )
{
	Echo "Deregistering WebBinding from IIS"
	Remove-Item -path IIS:\SslBindings\0.0.0.0!443
}

# Install certificate.
Echo "Installing cert..."
$securepwd = ConvertTo-SecureString -String $password -Force -AsPlainText
$cert = Import-PfxCertificate -FilePath "C:\certification.pfx" cert:\localMachine\my -Password $securepwd
# Create site binding.
Echo "Creating and registering WebBinding"
New-WebBinding -Name "Default Web Site" -IP "*" -Port 443 -Protocol https
New-Item -path IIS:\SslBindings\0.0.0.0!443 -value $cert -Force
## (optional) Remove the HTTP binding - uncomment the following line to unbind port 80.
# Remove-WebBinding -Name "Default Web Site" -BindingInformation *:80:
# Update firewall.
netsh advfirewall firewall add rule name="Open port 443" protocol=TCP localport=443 action=allow dir=OUT

git clone https://github.com/tburnett80/parishRegistrationForm.git
cd  parishRegistrationForm/ParishForms/ParishForms
dotnet publish -c Release -o out /p:CopyOutputSymbolsToPublishDirectory=false
docker build -t parish-frm .

cd ../../../
rm -rf parishRegistrationForm/
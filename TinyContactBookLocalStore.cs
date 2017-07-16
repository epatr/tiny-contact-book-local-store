using SnapsLibrary;

class Ch08_08_TinyContactBookLocalStore
{
    string TidyInput(string input)
    {
        input = input.Trim();
        input = input.ToLower();
        return input;
    }

    bool FetchContact(string name, out string address, out string phone)
    {
        name = TidyInput(name);

        address = SnapsEngine.FetchStringFromLocalStorage(itemName: name + ":address");
        phone = SnapsEngine.FetchStringFromLocalStorage(itemName: name + ":phone");

        if (address == null || phone == null) return false;

        return true;
    }

    void StoreContact(string name, string address, string phone)
    {
        name = TidyInput(name);

        SnapsEngine.SaveStringToLocalStorage(itemName: name + ":address",
                                            itemValue: address);
        SnapsEngine.SaveStringToLocalStorage(itemName: name + ":phone",
                                            itemValue: phone);
    }

    void NewContact()
    {
        SnapsEngine.DisplayString("Enter the contact");
        string name = SnapsEngine.ReadString("Enter new contact name");
        string address = SnapsEngine.ReadMultiLineString("Enter contact address");
        string phone = SnapsEngine.ReadString("Enter contact phone");
        StoreContact(name: name, address: address, phone: phone);
    }

void FindContact()
{
    string name = SnapsEngine.ReadString("Enter contact name");

    string contactAddress, contactPhone;

    if (FetchContact(name: name, address: out contactAddress, phone: out contactPhone))
    {
        SnapsEngine.ClearTextDisplay();

        SnapsEngine.AddLineToTextDisplay("Name: " + name);
        SnapsEngine.AddLineToTextDisplay("Address: " + contactAddress);
        SnapsEngine.AddLineToTextDisplay("Phone: " + contactPhone);

    }
    else
    {
        SnapsEngine.DisplayString("Name not found");
    }

    SnapsEngine.WaitForButton("Continue");

    SnapsEngine.ClearTextDisplay();
}

    public void StartProgram()
    {
        while (true)
        {
            SnapsEngine.SetTitleString("Tiny Contacts");

            string command = SnapsEngine.SelectFrom2Buttons("New Contact", "Find Contact");

            if (command == "New Contact")
            {
                NewContact();
            }

            if (command == "Find Contact")
            {
                FindContact();
            }
        }
    }
}

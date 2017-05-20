#include "LiftPanel.h"
namespace Lift_СPP_Version
{
	LiftPanel::~LiftPanel()
	{
		
	
		int n=this->buttons->Length;
		for (int i = 0; i < n; i++)
        {
            this->buttons[i]->Click -= gcnew EventHandler(this, &LiftPanel::MyLift_Click);
			delete buttons[i];
        }
		delete buttons;

        button_call->Click -= gcnew EventHandler(this, &LiftPanel::button_call_Click);
        button_open->Click -= gcnew EventHandler(this, &LiftPanel::button_open_Click);
        button_close->Click -= gcnew EventHandler(this, &LiftPanel::button_close_Click);
		delete button_close;
		delete button_open;
		delete button_call;

		
	}

	LiftPanel::LiftPanel(Panel^& Contol_panel, int n)
    {
        this->Contol_panel = Contol_panel;

        this->buttons = gcnew array<Button^>(n);
        this->button_close = gcnew Button();
        this->button_open = gcnew Button();
        this->button_call = gcnew Button();
            

        CreateForm();
            

        for (int i = 0; i < n; i++)
        {

            buttons[i] = gcnew Button();
            this->Contol_panel->Controls->Add(buttons[i]);

            this->buttons[i]->Location = Point(4, (n - i - 1) * 25 + 4 * (n - i - 1) + 25);
            this->buttons[i]->Size = Size(42, 23);
            this->buttons[i]->TabIndex = i;
            this->buttons[i]->Text = (i + 1).ToString();
            this->buttons[i]->UseVisualStyleBackColor = true;
            this->buttons[i]->Click += gcnew EventHandler(this, &LiftPanel::MyLift_Click);
        }
        button_call->Click += gcnew EventHandler(this, &LiftPanel::button_call_Click);
        button_open->Click += gcnew EventHandler(this, &LiftPanel::button_open_Click);
        button_close->Click += gcnew EventHandler(this, &LiftPanel::button_close_Click);

    }
	void LiftPanel::button_call_Click(Object^ sender, EventArgs^ e)
    {
        Console::Beep(1000, 100);
    }

    void LiftPanel::button_close_Click(Object^ sender, EventArgs^ e)
    {
        CloseD();
    }

    void LiftPanel::button_open_Click(Object^ sender, EventArgs^ e)
    {
        OpenD();
    }

    void LiftPanel::MyLift_Click(Object^ sender, EventArgs^ e)
    {
        Button^ temp = safe_cast<Button^>(sender);
        int _numb = 0;
        for (int i = 0; i < buttons->Length; i++)
        {
            if (buttons[i] == temp)
            {
                _numb = i;
            }
        }
        if (buttons[_numb]->BackColor == Color::Red)
            reset(_numb);
        else
            buttons[_numb]->BackColor = Color::Red;
        CallLift(_numb);
    }
	void LiftPanel::reset(int i)
    {
        buttons[i]->BackColor = Control::DefaultBackColor;
    }
	//Кусок макета
	void LiftPanel::CreateForm()
	{
		Label^ label1 = gcnew Label();
		// 
		// Contol_panel
		// 
		this->Contol_panel->Controls->Add(this->button_close);
		this->Contol_panel->Controls->Add(this->button_open);
		this->Contol_panel->Controls->Add(this->button_call);
		this->Contol_panel->Controls->Add(label1);
		this->Contol_panel->Name = "Contol_panel";
		this->Contol_panel->TabIndex = 1;
		this->Contol_panel->BackColor = Color::White;
		// 
		// label1
		// 
		label1->AutoSize = true;
		label1->Location = Point(42, 4);
		label1->Name = "label1";
		label1->Size =  Size(69, 13);
		label1->TabIndex = 0;
		label1->Text = "Управление";

		// 
		// button_open
		// 
		this->button_open->Location =  Point(127, 25);
		this->button_open->Name = "button_open";
		this->button_open->Size =  Size(42, 23);
		this->button_open->TabIndex = 3;
		this->button_open->Text = "Open";
		this->button_open->UseVisualStyleBackColor = true;


		this->button_call->Location =  Point(127, 80);
		this->button_call->Name = "button_call";
		this->button_call->Size =  Size(42, 23);
		this->button_call->TabIndex = 3;
		this->button_call->Text = "Call";
		this->button_call->UseVisualStyleBackColor = true;
		// 
		// button_close
		// 
		this->button_close->Location =  Point(127, 54);
		this->button_close->Name = "button_close";
		this->button_close->Size =  Size(42, 23);
		this->button_close->TabIndex = 4;
		this->button_close->Text = "Close";
		this->button_close->UseVisualStyleBackColor = true;
	}
}
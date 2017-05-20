#include "Floor.h"

namespace Lift_СPP_Version
{
	Floor::Floor(Panel^& pn, int Number_floor)
	{
		
		this->floor_panel = pn;
		this->floor_index = Number_floor;

		this->floor_label = gcnew Label();
		this->lift_label = gcnew Label();
		this->butt_up = gcnew Button();
		this->butt_down = gcnew Button();
          
		//Кусок макета
		CreateForm();
            
		this->floor_label->Text = (this->floor_index+1).ToString();
			
		//подписываемся на события
		this->butt_down->Click += gcnew System::EventHandler(this,&Floor::butt_down_Click);
		this->butt_up->Click += gcnew System::EventHandler(this, &Floor::butt_up_Click);
	}

	Floor::~Floor()
	{
		this->butt_down->Click -= gcnew System::EventHandler(this,&Floor::butt_down_Click);
		this->butt_up->Click -= gcnew System::EventHandler(this, &Floor::butt_up_Click);

		delete floor_label;			
		delete lift_label;
		delete butt_up;
		delete butt_down;
	}

	//изменение указалеля положения лифта
	void Floor::ChangeLiftPositionLabel(int i)
    {
        this->lift_label->Text = (i + 1).ToString();
    }

    //Уровень для проверки на прибытия на этаж
	int Floor::GetHight()
    {
        return this->floor_panel->Location.Y;
    }

	//Сброс цвета кнопки вниз
	void Floor::resetDown()
    {
        this->butt_down->BackColor = Control::DefaultBackColor;
    }

	//Сброс цвета кнопки вверх
	void Floor::resetUP()
    {
        this->butt_up->BackColor = Control::DefaultBackColor;
    }

	//Видимость кнопки вверх
    void Floor::UpVisible(bool value)
    {
		this->butt_up->Visible = value;
    }

	//Видимость кнопки вниз
    void Floor::DownVisible(bool value)
    {
		this->butt_down->Visible = value; 
    }

	void Floor::butt_up_Click(Object^ sender, EventArgs^ e)
    {
        this->butt_up->BackColor = Color::Red;
        CallUp(floor_index);
    }

    void Floor::butt_down_Click(Object^ sender, EventArgs^ e)
    {
		((Button^)sender)->BackColor = Color::Red;
        CallDown(floor_index);
    }

	//Кусок макета
	void Floor::CreateForm()
	{
		// 
        // panel
        // 
        this->floor_panel->Controls->Add(this->lift_label);
        this->floor_panel->Controls->Add(this->butt_down);
        this->floor_panel->Controls->Add(this->butt_up);
        this->floor_panel->Controls->Add(this->floor_label);
        this->floor_panel->TabIndex = 0;
		this->floor_panel->Size.Height=100;
		this->floor_panel->Size.Width=100;
		this->floor_panel->BackColor=Color::White;
        // 
        // floor_label
        // 
        this->floor_label->AutoSize = true;
		this->floor_label->Location = Point(3,23);
        this->floor_label->Name = "floor_label";
        this->floor_label->Size = Size(35, 13);
        this->floor_label->TabIndex = 0;
        this->floor_label->Text = "floor_label";
        // 
        // butt_up
        // 
        this->butt_up->Location = Point(6, 40);
        this->butt_up->Name = "butt_up";
        this->butt_up->Size = Size(32, 23);
        this->butt_up->TabIndex = 1;
        this->butt_up->Text = "up";
        this->butt_up->UseVisualStyleBackColor = true;
        // 
        // butt_down
        // 
        this->butt_down->Location = Point(6, 69);
        this->butt_down->Name = "butt_down";
        this->butt_down->Size = Size(45, 23);
        this->butt_down->TabIndex = 2;
        this->butt_down->Text = "Down";
        this->butt_down->UseVisualStyleBackColor = true;
        // 
        // lift_label
        // 
        this->lift_label->AutoSize = true;
        this->lift_label->Location = Point(35, 10);
        this->lift_label->Name = "lift_label";
        this->lift_label->Size = Size(35, 13);
        this->lift_label->TabIndex = 3;
        this->lift_label->Text = "0";
	}
}

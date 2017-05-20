#include "Cabine.h"

namespace Lift_ÑPP_Version
{
	
	Cabine::Cabine(Panel^& Shakhta,int N)
	{
		this->N=N;
		this->Shakhta=Shakhta;
		this->g=this->Shakhta->CreateGraphics();
		this->cabin.Location = Point(0, 0);
		this->cabin.Size = Size(Shakhta->Width, 100);
		this->status=Cabine_Status::Opened;
		this->t = gcnew Timer();
        this->t->Interval = 200;
        this->t->Tick += gcnew System::EventHandler(this,&Cabine::t_Tick);

		Button^ bt=gcnew Button();
		this->Shakhta->Controls->Add(bt);
		bt->Location=Point(15,955);
		DrawCabine();
		t->Start();
	}

	void Cabine::DrawCabine()
    {
		this->g=Shakhta->CreateGraphics();
        Brush^ br = gcnew SolidBrush(Color::Blue);
		Shakhta->Refresh();
			

		if (this->status == Cabine_Status::Opened)
        {
            this->g->FillRectangle(br, cabin);
            Brush^ br2 = gcnew SolidBrush(Color::White);
            this->g->FillRectangle(br2, cabin.Location.X + 15, cabin.Location.Y, cabin.Width-30,cabin.Height);
               
        }
		if (status == Cabine_Status::Closed)            
        {
            this->g->FillRectangle(br, cabin);
        }	
    }

	void Cabine::stopping()
    {
		command= Brain_Status::DontMove;
		status = Cabine_Status::Opened;
        DrawCabine();
		counter=0;
		t->Tick -= gcnew System::EventHandler(this,&Cabine::t_Tick);
        t->Tick+=gcnew System::EventHandler(this,&Cabine::t2_Tick);
    }

        

    void Cabine::t2_Tick(Object^ sender, EventArgs^ e)
    {
        counter++;
        if (counter == 5)
        {
            t->Tick +=gcnew System::EventHandler(this,&Cabine:: t_Tick);
            t->Tick -= gcnew System::EventHandler(this,&Cabine::t2_Tick);				               
			status = Cabine_Status::Closed;
            DrawCabine();
            counter = 0;
        }
    }
        
    void Cabine::t_Tick(Object^ sender, EventArgs^ e)
    {
        int dy=0;
		switch (command)
        {
			case (Brain_Status::DontMove): dy = 0; DrawCabine(); return; 
			case (Brain_Status::GoDown): dy=10; break;
			case (Brain_Status::GoUp): dy = -10; break;

        }
        this->cabin.Location = Point(cabin.Location.X, cabin.Location.Y + dy);
        if (cabin.Location.Y <= 0)
        {
            cabin.Location = Point(cabin.Location.X, 0);
			command = Brain_Status::GoDown;
        }

        if (cabin.Location.Y > ((N-1)*100+(N-1)*4)+15)
        {
            cabin.Location = Point(cabin.Location.X, ((N - 1) * 100 + (N - 1) * 4));
			command=Brain_Status::GoUp;
        }

        DrawCabine();
        CheckIsCabineOnFloor();
    }

		

	void Cabine::CheckIsCabineOnFloor()
    {
        for (int i = 0; i < N+1; i++)
        {
            int level = ((i-1) * 100 + (i-1) * 4);

			if (Math::Abs(cabin.Location.Y -level )<5)
            {
                cabinlevel = N-i;                   
                ChangeFloor(N-i);
                break;
            }
        }
    }

    void Cabine::c_pan_OpenD()
    {
		this->status=Cabine_Status::Opened;
        DrawCabine();
    }

    void Cabine::c_pan_CloseD()
    {
		this->status=Cabine_Status::Closed;
        DrawCabine();
    }

	void Cabine::brain_new_command(int new_command)
	{
		command=new_command;
	}

}
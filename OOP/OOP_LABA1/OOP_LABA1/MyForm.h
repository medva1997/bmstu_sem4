#pragma once
#include "Data.h"
#include <math.h>
namespace OOP_LABA1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Runtime::InteropServices;

	/// <summary>
	/// Сводка для MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		struct Data *datapack;
		Pen^ blackPen;
		Pen^ redPen ;
		MyForm(void)
		{
			InitializeComponent();
			blackPen = gcnew Pen( Color::Black,1.0f );
			redPen = gcnew Pen( Color::Red,1.0f );
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Panel^  panel1;
	protected: 
	private: System::Windows::Forms::OpenFileDialog^  openFileDialog1;
	private: System::Windows::Forms::Button^  button1;
	private: System::Windows::Forms::Button^  button2;
	private: System::Windows::Forms::Button^  button3;
	private: System::Windows::Forms::Button^  button4;
	private: System::Windows::Forms::Button^  button5;

	private:
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->panel1 = (gcnew System::Windows::Forms::Panel());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->button3 = (gcnew System::Windows::Forms::Button());
			this->button4 = (gcnew System::Windows::Forms::Button());
			this->button5 = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// panel1
			// 
			this->panel1->BackColor = System::Drawing::SystemColors::ActiveCaption;
			this->panel1->Location = System::Drawing::Point(12, 12);
			this->panel1->Name = L"panel1";
			this->panel1->Size = System::Drawing::Size(677, 490);
			this->panel1->TabIndex = 0;
			this->panel1->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &MyForm::panel1_MouseDown);
			this->panel1->MouseMove += gcnew System::Windows::Forms::MouseEventHandler(this, &MyForm::panel1_MouseMove);
			this->panel1->MouseUp += gcnew System::Windows::Forms::MouseEventHandler(this, &MyForm::panel1_MouseUp);
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			this->openFileDialog1->FileOk += gcnew System::ComponentModel::CancelEventHandler(this, &MyForm::openFileDialog1_FileOk);
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(696, 12);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(130, 23);
			this->button1->TabIndex = 1;
			this->button1->Text = L"Load file";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// button2
			// 
			this->button2->Enabled = false;
			this->button2->Location = System::Drawing::Point(788, 86);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(75, 44);
			this->button2->TabIndex = 2;
			this->button2->Text = L"▲";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &MyForm::button2_Click);
			// 
			// button3
			// 
			this->button3->Enabled = false;
			this->button3->Location = System::Drawing::Point(788, 136);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(75, 44);
			this->button3->TabIndex = 3;
			this->button3->Text = L"▼";
			this->button3->UseVisualStyleBackColor = true;
			this->button3->Click += gcnew System::EventHandler(this, &MyForm::button3_Click);
			// 
			// button4
			// 
			this->button4->Enabled = false;
			this->button4->Location = System::Drawing::Point(695, 118);
			this->button4->Name = L"button4";
			this->button4->Size = System::Drawing::Size(75, 44);
			this->button4->TabIndex = 4;
			this->button4->Text = L"<";
			this->button4->UseVisualStyleBackColor = true;
			this->button4->Click += gcnew System::EventHandler(this, &MyForm::button4_Click);
			// 
			// button5
			// 
			this->button5->Enabled = false;
			this->button5->Location = System::Drawing::Point(869, 118);
			this->button5->Name = L"button5";
			this->button5->Size = System::Drawing::Size(75, 44);
			this->button5->TabIndex = 5;
			this->button5->Text = L">";
			this->button5->UseVisualStyleBackColor = true;
			this->button5->Click += gcnew System::EventHandler(this, &MyForm::button5_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(972, 512);
			this->Controls->Add(this->button5);
			this->Controls->Add(this->button4);
			this->Controls->Add(this->button3);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->panel1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->FormClosed += gcnew System::Windows::Forms::FormClosedEventHandler(this, &MyForm::MyForm_FormClosed);
			this->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::MyForm_KeyPress);
			this->ResumeLayout(false);

		}
#pragma endregion
		private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e){

					 openFileDialog1->ShowDialog();
			 }
	private: System::Void MyForm_KeyPress(System::Object^  sender, System::Windows::Forms::KeyPressEventArgs^  e) {
				
			 }
	private: System::Void button2_Click(System::Object^  sender, System::EventArgs^  e) {
				Drawing(-10, 2);
			 }

	private: System::Void button3_Click(System::Object^  sender, System::EventArgs^  e) {
					Drawing(10, 2);
			 }

	private: System::Void button5_Click(System::Object^  sender, System::EventArgs^  e) {
					Drawing(-10, 1);
			 }

	private: System::Void button4_Click(System::Object^  sender, System::EventArgs^  e) {
					Drawing(10, 1);				
			 }

		 private: System::Void Drawing(double angle, int type)
		 {
			 if(datapack==NULL)
					return;
				Graphics^ g=panel1->CreateGraphics(); 
				g->FillRectangle(gcnew SolidBrush(Color::Azure),0,0,panel1->Width,panel1->Height);
				datapack->DrawCentre=new PointF((float)(panel1->Width/2),(float)(panel1->Height/2));
				if(type==1)
					TurnByY(angle*Math::PI/180,datapack);
				else
					TurnByX(angle*Math::PI /180,datapack);
				Draw(g,redPen,datapack);
		 }

private: System::Void openFileDialog1_FileOk(System::Object^  sender, System::ComponentModel::CancelEventArgs^  e) {
					datapack=(struct Data *) malloc(sizeof(struct Data));
					char *filename=(char*)(void*)Marshal::StringToHGlobalAnsi(openFileDialog1->FileName);
					LoadFile(filename,datapack);	
					button2->Enabled=true;
					button3->Enabled=true;
					button4->Enabled=true;
					button5->Enabled=true;
					Drawing(0, 1);
		 }
private: System::Void panel1_MouseMove(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) {

			 /*if(datapack!=NULL)
			 {
				 double dx=MouseLocation.X-e->Location.X;
				 double dy=MouseLocation.Y-e->Location.Y;

				 Drawing(-dx/10, 1);	
				 Drawing(dy/10, 2);	
				 MouseLocation=e->Location;
			 }
			 */
		 }


		 private: PointF MouseLocation;
private: System::Void panel1_MouseUp(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) {
			 double dx=MouseLocation.X-e->Location.X;
			 double dy=MouseLocation.Y-e->Location.Y;

			 Drawing(-dx/10, 1);	
			 Drawing(dy/10, 2);	
			 MouseLocation=e->Location;


		 }
private: System::Void panel1_MouseDown(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) {
			 MouseLocation=e->Location;
		 }

private: System::Void MyForm_FormClosed(System::Object^  sender, System::Windows::Forms::FormClosedEventArgs^  e) {

			 freedata(datapack);
			 free(datapack);
		 }
};
}

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
	/// —водка дл€ MyForm
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
		/// ќсвободить все используемые ресурсы.
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
		/// “ребуетс€ переменна€ конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// ќб€зательный метод дл€ поддержки конструктора - не измен€йте
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
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(696, 12);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(130, 23);
			this->button1->TabIndex = 1;
			this->button1->Text = L"button1";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(788, 86);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(75, 44);
			this->button2->TabIndex = 2;
			this->button2->Text = L"button2";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &MyForm::button2_Click);
			// 
			// button3
			// 
			this->button3->Location = System::Drawing::Point(788, 136);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(75, 44);
			this->button3->TabIndex = 3;
			this->button3->Text = L"button3";
			this->button3->UseVisualStyleBackColor = true;
			this->button3->Click += gcnew System::EventHandler(this, &MyForm::button3_Click);
			// 
			// button4
			// 
			this->button4->Location = System::Drawing::Point(695, 118);
			this->button4->Name = L"button4";
			this->button4->Size = System::Drawing::Size(75, 44);
			this->button4->TabIndex = 4;
			this->button4->Text = L"button4";
			this->button4->UseVisualStyleBackColor = true;
			this->button4->Click += gcnew System::EventHandler(this, &MyForm::button4_Click);
			// 
			// button5
			// 
			this->button5->Location = System::Drawing::Point(869, 118);
			this->button5->Name = L"button5";
			this->button5->Size = System::Drawing::Size(75, 44);
			this->button5->TabIndex = 5;
			this->button5->Text = L"button5";
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
			this->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::MyForm_KeyPress);
			this->ResumeLayout(false);

		}
#pragma endregion
		private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e){

					 openFileDialog1->ShowDialog();
					 datapack=(struct Data *) malloc(sizeof(struct Data));
					 char *filename=(char*)(void*)Marshal::StringToHGlobalAnsi(openFileDialog1->FileName);
					 LoadFile(filename,datapack);					 
					Drawing(0, 1);
					 
					 
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
				Graphics^ g=panel1->CreateGraphics(); 
				g->FillRectangle(gcnew SolidBrush(Color::Azure),0,0,panel1->Width,panel1->Height);
				datapack->DrawCentre=new PointF(panel1->Width/2,panel1->Height/2);

				if(type==1)
					TurnByY(angle*Math::PI/180,datapack);
				else
					TurnByX(angle*Math::PI /180,datapack);
				Draw(g,redPen,datapack);
		 }
};
}

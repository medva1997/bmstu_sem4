#include "mycontroller.h"
#include "ui_mycontroller.h"
#include <QRegExpValidator>
#include <QIntValidator>
#include <QFileDialog>
#include <iostream>
#include <string.h>
#include "actiongeometry.h"
#include "action.h"


#define MIN_PAR 10
Text_Error LineEditError;


MyController::MyController(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::MyController)
{
    this->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);    // Растягиваем содержимое по виджету

    par = parent;
    ui->setupUi(this);
    Validator = new QRegExpValidator(QRegExp("^[+-]?[0-9]{0,5}(\\.|,|$)[0-9]{0,4}$"));
    ui->dxEdit->setValidator(Validator);
    ui->dyEdit->setValidator(Validator);
    ui->dzEdit->setValidator(Validator);
    ui->kxEdit->setValidator(Validator);

    ui->rot_xEdit->setValidator(Validator);
    ui->rot_yEdit->setValidator(Validator);
    ui->rot_zEdit->setValidator(Validator);
}
void MyController::GetScene(My_Scene *scene1) {
    this->scene.height = scene1->height;
    this->scene.width = scene1->width;
    this->scene.scene = scene1->scene;

}

MyController::~MyController()
{
    argument act;
    CleanAction(act.modify_act);
    processor(act, CLEAR);
    delete Validator;    
    delete ui;
}

//получение данных с выбранных полей
double * MyController::GetData(vector <QLineEdit*> &vec)
{
    QString str;
    double *data = new double[vec.size()];
    double x;
    QString mess;
    for(unsigned int i = 0; i < vec.size(); i++) {
        str = vec[i]->text();
        x = Analiz_Text(str);
        switch(LineEditError)
        {
        case EMPTY:
            mess = "Область текста пуста.\n Введите данные!";
            break;
        case E_SYMBOL:
            mess = "Ошибочный символ\n "
                   "Разрешается использование только цифр, '.'',' и знаков табуляции";
            break;
        }
        if(LineEditError != NO_ER) {
            QErrorMessage errorMessage;
            errorMessage.showMessage(mess);
            errorMessage.exec();
            break;
        }
        data[i] = x;
    }
   return data;
}
//анализ строки данных (проверка, явл-тся ли вещественным числом)
double Analiz_Text(QString str)
{
    LineEditError = NO_ER;
    if(str == "") {
        LineEditError = EMPTY;
        return 0;
    }
    str.replace(QString(","), QString("."));
    //str.replace(QString("\n"), QString(" "));

    QStringList list = str.split(' ', QString::SkipEmptyParts);
    if(list.size() > 1) {
        LineEditError = E_SYMBOL;
        return 0;
    }

    double x;
    bool ok = true;
    for(int i = 0; i < list.size(); i++) {
        x = list.at(i).toDouble(&ok);
        //cout << tmp.toStdString() << " " << ok << " " << x << endl;
        if(!ok) {
            LineEditError = E_SYMBOL;
            return 0;
        }
    }
    return x;
}


void MyController::on_rotateButton_clicked()
{

    vector<QLineEdit*> edits;
    edits.push_back(ui->rot_xEdit);
    edits.push_back(ui->rot_yEdit);
    edits.push_back(ui->rot_zEdit);   

    double *data = GetData(edits);
    if(LineEditError != NO_ER)
        return;

    argument act;
    CleanAction(act.modify_act);
    act.modify_act.angleOX = data[0];
    act.modify_act.angleOY = data[1];
    act.modify_act.angleOZ = data[2];
    processor(act, CHANGE);
    CleanAction(act.modify_act);

    act.draw_act.height =scene.height;
    act.draw_act.width = scene.width;
    act.draw_act.scene = scene.scene;
    processor(act, DRAW);
    delete[] data;
}

void MyController::on_scaleButton_clicked()
{

    vector<QLineEdit*> edits;
    edits.push_back(ui->kxEdit);


    double *data = GetData(edits);
    if(LineEditError != NO_ER)
        return;

    argument act;
    CleanAction(act.modify_act);
    act.modify_act.scale = data[0];
    processor(act, CHANGE);
    CleanAction(act.modify_act);

    act.draw_act.height =scene.height;
    act.draw_act.width = scene.width;
    act.draw_act.scene = scene.scene;
    processor(act, DRAW);
    delete[] data;

}

void MyController::on_moveButton_clicked()
{
    vector<QLineEdit*> edits;
    edits.push_back(ui->dxEdit);
    edits.push_back(ui->dyEdit);
    edits.push_back(ui->dzEdit);

    double *data = GetData(edits);
    if(LineEditError != NO_ER)
        return;

    argument act;
    CleanAction(act.modify_act);
    act.modify_act.moveOX = data[0];
    act.modify_act.moveOY = data[1];
    act.modify_act.moveOX = data[2];
    processor(act, CHANGE);
    CleanAction(act.modify_act);

    act.draw_act.height =scene.height;
    act.draw_act.width = scene.width;
    act.draw_act.scene = scene.scene;
    processor(act, DRAW);
    delete[] data;
}



void MyController::on_fileButton_clicked()
{

    QString str = QFileDialog::getOpenFileName(0, "Open Dialog", "", "*.txt");
    if(str == "")
        return;
    argument act;
    CleanAction(act.modify_act);
    //magic
    QByteArray ba = str.toLatin1();
    act.load_act = ba.data();

    //act.load_act="/home/alexey/file1.txt";
    int  res=processor(act, LOAD);

    QString mess = "";
    if(res == ERROR_FILE) {
        mess = "Cannot open file";
    } else if(res == ERROR_WITH_MEMORY) {
        mess = "Memory error";
    }
    if(mess != "") {
        QErrorMessage errorMessage;
        errorMessage.showMessage(mess);
        errorMessage.exec();
        return;
    }

    act.draw_act.height =scene.height;
    act.draw_act.width = scene.width;
    act.draw_act.scene = scene.scene;
    processor(act, DRAW);
}




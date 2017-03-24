#ifndef MYCONTROLLER_H
#define MYCONTROLLER_H

#include <QWidget>
#include <QtGui>
#include <QErrorMessage>
#include <QString>
#include <QStringList>
#include <string>
#include <stdio.h>
#include <vector>
#include <QLineEdit>
#include <QGraphicsScene>
#include "myprocessor.h"

using namespace std;
enum Text_Error { EMPTY, E_SYMBOL, NO_ER };


namespace Ui {
class MyController;
}

class MyController : public QWidget
{
    Q_OBJECT

public:
    explicit MyController(QWidget *parent = 0);
    ~MyController();
    void GetScene(My_Scene *scene);
signals:
    void SceneChange(QGraphicsScene *scene);
private slots:
    void on_rotateButton_clicked();

    void on_scaleButton_clicked();

    void on_moveButton_clicked();

    void on_fileButton_clicked();


private:

    double *GetData(vector <QLineEdit*> &vec);
    My_Scene scene;
    Ui::MyController *ui;
    QWidget *par;
    QRegExpValidator *Validator;

};

double Analiz_Text(QString str);
#endif // MYCONTROLLER_H

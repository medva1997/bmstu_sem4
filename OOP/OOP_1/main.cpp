#include "mainwindow.h"
#include <QApplication>
#include "Model.h"
#include <stdlib.h>
#include <stdio.h>
#include "actiongeometry.h"
#include "action.h"


int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();
    return a.exec();
}

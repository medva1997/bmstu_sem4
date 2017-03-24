#include "mygraphicview.h"
#include "ui_mygraphicview.h"
#include <iostream>

MyGraphicView::MyGraphicView(QWidget *parent) :
     QGraphicsView(parent),
    ui(new Ui::MyGraphicView)
{
    this->setHorizontalScrollBarPolicy(Qt::ScrollBarAlwaysOff);
    this->setVerticalScrollBarPolicy(Qt::ScrollBarAlwaysOff);

    this->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);    // Растягиваем содержимое по виджету

    my_scene.scene = new QGraphicsScene();   // Инициализируем сцену для отрисовки
    this->setScene(my_scene.scene);          // Устанавливаем сцену в виджет
    int width = this->width();
    int height = this->height();
    my_scene.scene->setSceneRect(0,0,width,height);
    //my_scene.x_center = width / 2;
    //my_scene.y_center = height / 2;
    ui->setupUi(this);
}

void MyGraphicView::Connect()
{
    emit SendScene(&my_scene);
}

MyGraphicView::~MyGraphicView()
{
    delete my_scene.scene;
    delete ui;
}

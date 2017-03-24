#-------------------------------------------------
#
# Project created by QtCreator 2017-03-15T14:37:50
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = LeshaTest
TEMPLATE = app

# The following define makes your compiler emit warnings if you use
# any feature of Qt which as been marked as deprecated (the exact warnings
# depend on your compiler). Please consult the documentation of the
# deprecated API in order to know how to port your code away from it.
DEFINES += QT_DEPRECATED_WARNINGS

# You can also make your code fail to compile if you use deprecated APIs.
# In order to do so, uncomment the following line.
# You can also select to disable deprecated APIs only up to a certain version of Qt.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0


SOURCES += main.cpp\
        mainwindow.cpp \
    memory.cpp \
    Counts.cpp \
    Edge.cpp \
    Point.cpp \
    Edges.cpp \
    FileWorker.cpp \
    Points.cpp \
    Model.cpp \
    mypointmath.cpp \
    geometry.cpp \
    actiongeometry.cpp \
    action.cpp \
    mycontroller.cpp \
    mygraphicview.cpp \
    myprocessor.cpp \
    drawing.cpp

HEADERS  += mainwindow.h \
    memory.h \
    Error.h \
    Counts.h \
    Edges.h \
    Points.h \
    Point.h \
    Edge.h \
    FileWorker.h \
    Model.h \
    mypointmath.h \
    geometry.h \
    actiongeometry.h \
    action.h \
    mycontroller.h \
    mygraphicview.h \
    my_scene.h \
    myprocessor.h \
    drawing.h

FORMS    += mainwindow.ui \
    mycontroller.ui \
    mygraphicview.ui

#include <lib/Array17_3D.h>
#include <gtest/gtest.h>
#include <sstream>
#include <string>


TEST(Array17_3dTestSuite, target_input_test) {
    Array_3D arg;

    arg.MakeArray(10, 10, 10);
    arg[0][0][0] = 1;
    arg[0][0][1] = 6;
    arg[0][0][2] = 24;
    arg[0][0][3] = 112;
    arg[0][0][4] = 257;
    arg[0][0][5] = 400;
    arg[0][0][6] = 518;
    arg[0][0][7] = 1024;
    arg[0][0][8] = 2000;
    arg[0][5][0] = 123423;
    arg[0][1][0] = 1488;
    arg[9][9][9] = 5051;

    ASSERT_EQ(arg[0][0][0] * 1, 1);
    ASSERT_EQ(arg[0][0][1] * 1, 6);
    ASSERT_EQ(arg[0][0][2] * 1, 24);
    ASSERT_EQ(arg[0][0][3] * 1, 112);
    ASSERT_EQ(arg[0][0][4] * 1, 257);
    ASSERT_EQ(arg[0][0][5] * 1, 400);
    ASSERT_EQ(arg[0][0][6] * 1, 518);
    ASSERT_EQ(arg[0][0][7] * 1, 1024);
    ASSERT_EQ(arg[0][0][8] * 1, 2000);
    ASSERT_EQ(arg[0][5][0] * 1, 123423);
    ASSERT_EQ(arg[0][1][0] * 1, 1488);
    ASSERT_EQ(arg[9][9][9] * 1, 5051);
}

TEST(Array17_3dTestSuite, multiplication_test) {
    Array_3D arg;

    arg.MakeArray(1, 5, 2);//Minecraft, bitch, is my life! Minecraaaaaaft!

    arg[0][0][0] = 1;
    arg[0][0][1] = 6;
    arg[0][1][0] = 121235;
    arg[0][1][1] = 12325;
    arg[0][2][0] = 51835;
    arg[0][2][1] = 10024;
    arg[0][3][0] = 2000;
    arg[0][3][1] = 123;
    arg[0][4][0] = 1488;
    arg[0][4][1] = 5051;

    arg = arg * 2;

    ASSERT_EQ(arg[0][0][0] * 1, 2);
    ASSERT_EQ(arg[0][0][1] * 1, 12);
    ASSERT_EQ(arg[0][1][1] * 1, 12325 * 2);
    ASSERT_EQ(arg[0][2][0] * 1, 51835 * 2);
    ASSERT_EQ(arg[0][2][1] * 1, 10024 * 2);
    ASSERT_EQ(arg[0][3][0] * 1, 2000 * 2);
    ASSERT_EQ(arg[0][3][1] * 1, 123 * 2);
    ASSERT_EQ(arg[0][4][0] * 1, 1488 * 2);
    ASSERT_EQ(arg[0][4][1] * 1, 5051 * 2);

    ASSERT_NE(arg[0][1][0] * 1, 121235 * 2);
}

TEST(Array17_3dTestSuite, plus_test) {
    Array_3D arg;
    Array_3D arg2;

    arg.MakeArray(1, 5, 2);//Minecraft, bitch, is my life! Minecraaaaaaft!
    arg2.MakeArray(1, 5, 2);
    Array_3D arg3;
    arg3.MakeArray(1, 5, 2);


    arg[0][0][0] = 1;
    arg[0][0][1] = 6;
    arg[0][1][0] = 121235;
    arg[0][1][1] = 12325;
    arg[0][2][0] = 51835;
    arg[0][2][1] = 10024;
    arg[0][3][0] = 2000;
    arg[0][3][1] = 123;
    arg[0][4][0] = 1488;
    arg[0][4][1] = 5051;

    arg2[0][0][0] = 10;
    arg2[0][0][1] = 60;
    arg2[0][1][0] = 235;
    arg2[0][1][1] = 19325;
    arg2[0][2][0] = 9835;
    arg2[0][2][1] = 1724;
    arg2[0][3][0] = 9980;
    arg2[0][3][1] = 1231;
    arg2[0][4][0] = 89200;
    arg2[0][4][1] = 10011;

    arg3 = arg2 + arg;


    ASSERT_EQ(arg3[0][0][0] * 1, 11);
    ASSERT_EQ(arg3[0][0][1] * 1, 66);
    ASSERT_EQ(arg3[0][1][0] * 1, 121235 + 235);
    ASSERT_EQ(arg3[0][1][1] * 1, 12325 + 19325);
    ASSERT_EQ(arg3[0][2][0] * 1, 51835 + 9835);
    ASSERT_EQ(arg3[0][2][1] * 1, 10024 + 1724);
    ASSERT_EQ(arg3[0][3][0] * 1, 2000 + 9980);
    ASSERT_EQ(arg3[0][3][1] * 1, 123 + 1231);
    ASSERT_EQ(arg3[0][4][0] * 1, 1488 + 89200);
    ASSERT_EQ(arg3[0][4][1] * 1, 5051 + 10011);


}

TEST(Array17_3dTestSuite, minus_test) {
    Array_3D arg;
    Array_3D arg2;
    Array_3D arg3;

    arg.MakeArray(1, 5, 2);//Minecraft, bitch, is my life! Minecraaaaaaft!
    arg2.MakeArray(1, 5, 2);

    arg3.MakeArray(1, 5, 2);


    arg[0][0][0] = 100;
    arg[0][0][1] = 60;
    arg[0][1][0] = 121235;
    arg[0][1][1] = 20325;
    arg[0][2][0] = 51835;
    arg[0][2][1] = 10024;
    arg[0][3][0] = 20000;
    arg[0][3][1] = 123;
    arg[0][4][0] = 89200;
    arg[0][4][1] = 100051;

    arg2[0][0][0] = 10;
    arg2[0][0][1] = 60;
    arg2[0][1][0] = 235;
    arg2[0][1][1] = 19325;
    arg2[0][2][0] = 9835;
    arg2[0][2][1] = 1724;
    arg2[0][3][0] = 9980;
    arg2[0][3][1] = 1231;
    arg2[0][4][0] = 1488;
    arg2[0][4][1] = 10011;

    arg3 = arg - arg2;

    ASSERT_EQ(arg3[0][0][0] * 1, 90);
    ASSERT_EQ(arg3[0][0][1] * 1, 0);
    ASSERT_EQ(arg3[0][1][0] * 1, 121000);
    ASSERT_EQ(arg3[0][1][1] * 1, 1000);
    ASSERT_EQ(arg3[0][2][0] * 1, 42000);
    ASSERT_EQ(arg3[0][2][1] * 1, 8300);
    ASSERT_EQ(arg3[0][3][0] * 1, 10020);
    ASSERT_NE(arg3[0][3][1] * 1, 129963);
    ASSERT_EQ(arg3[0][4][0] * 1, 87712);
    ASSERT_EQ(arg3[0][4][1] * 1, 90040);
}

TEST(Array17_3dTestSuite, equating_test) {
    Array_3D arg;
    Array_3D arg2;

    arg.MakeArray(1, 5, 2);//Minecraft, bitch, is my life! Minecraaaaaaft!
    arg2.MakeArray(1, 5, 2);


    arg[0][0][0] = 100;
    arg[0][0][1] = 60;
    arg[0][1][0] = 121235;
    arg[0][1][1] = 20325;
    arg[0][2][0] = 51835;
    arg[0][2][1] = 10024;
    arg[0][3][0] = 20000;
    arg[0][3][1] = 123;
    arg[0][4][0] = 89200;
    arg[0][4][1] = 100051;

    arg2[0][0][0] = 10;
    arg2[0][0][1] = 60;
    arg2[0][1][0] = 235;
    arg2[0][1][1] = 19325;
    arg2[0][2][0] = 9835;
    arg2[0][2][1] = 1724;
    arg2[0][3][0] = 9980;
    arg2[0][3][1] = 1231;
    arg2[0][4][0] = 1488;
    arg2[0][4][1] = 10011;

    arg = arg - arg2;

    ASSERT_EQ(arg[0][0][0] * 1, 90);
    ASSERT_EQ(arg[0][0][1] * 1, 0);
    ASSERT_EQ(arg[0][1][0] * 1, 121000);
    ASSERT_EQ(arg[0][1][1] * 1, 1000);
    ASSERT_EQ(arg[0][2][0] * 1, 42000);
    ASSERT_EQ(arg[0][2][1] * 1, 8300);
    ASSERT_EQ(arg[0][3][0] * 1, 10020);
    ASSERT_NE(arg[0][3][1] * 1, 129963);
    ASSERT_EQ(arg[0][4][0] * 1, 87712);
    ASSERT_EQ(arg[0][4][1] * 1, 90040);
}

TEST(Array17_3dTestSuite, input_all_arr_test) {
    Array_3D arg;

    arg.MakeArray(1, 5, 2);//Minecraft, bitch, is my life! Minecraaaaaaft!

    std::stringstream string_in;
    std::stringstream string_out;
    std::string arr_in;
    std::string arr_sup;
    std::string arr_out;

    string_in<<"1 2 3 4 5 6 7 8 9 10";

    string_in>>arg;

    string_in.seekg(0, string_in.beg);

    for(int i = 0; i < 10; ++i) {
        string_in >> arr_sup;
        arr_in += " " + arr_sup;
    }

    string_out<<arg;

    for(int i = 0; i < 10; ++i) {
        string_out >> arr_sup;
        arr_out += " " + arr_sup;
    }

    ASSERT_EQ(arr_in, arr_out);
}
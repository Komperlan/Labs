#include "filesystem"
#include <string>
#include "fstream"
#pragma pack(push, 1)
#include <iostream>


void completion_bitmapinfoheader(uint8_t* bitmapinfoheader,const uint64_t width, const uint64_t height){
    uint8_t kInfoHeaderSize = 40;

    int kByte = 8;

    const int kBitsPerPixel = kByte/2;

    bitmapinfoheader[0] = static_cast<uint8_t>(kInfoHeaderSize);

    bitmapinfoheader[4] = static_cast<uint8_t>(width);
    bitmapinfoheader[5] = static_cast<uint8_t>(width>>kByte);
    bitmapinfoheader[6] = static_cast<uint8_t>(width>>(kByte*2));
    bitmapinfoheader[7] = static_cast<uint8_t>(width>>(kByte*3));

    bitmapinfoheader[8] = static_cast<uint8_t>(height);
    bitmapinfoheader[9] = static_cast<uint8_t>(height>>(kByte));
    bitmapinfoheader[10] = static_cast<uint8_t>(height>>(kByte*2));
    bitmapinfoheader[11] = static_cast<uint8_t>(height>>(kByte*3));

    bitmapinfoheader[12] = 1;

    bitmapinfoheader[14] = kBitsPerPixel;

    bitmapinfoheader[32] = 4;
}


void Create_bmp(uint64_t** field, const uint64_t  height, const uint64_t width,const std::string& file_path, const uint64_t count){

    int kByte = 8;

    uint64_t paddingSize = (4 - (width/2 + width % 2) % 4) % 4;

    uint64_t file_size = 14 + 40 + 20 + (height * ((width / 2 + width % 2) + paddingSize));

    std::string number = std::to_string(count);

    std::string filename = file_path + "Pic" + number + ".bmp";

    std::ofstream file(filename);



    static uint8_t header[] = {
            0, 0,
            0, 0, 0, 0,
            0, 0,
            0, 0,
            0, 0, 0, 0
    };


    header[0] = 'B';
    header[1] = 'M';
    header[2] = static_cast<uint8_t>(file_size);
    header[3] = static_cast<uint8_t>(file_size>>kByte);
    header[4] = static_cast<uint8_t>(file_size>>(kByte*2));
    header[5] = static_cast<uint8_t>(file_size>>(kByte*3));

    header[10] = static_cast<uint8_t>(74);

    file.write(reinterpret_cast<char*> (header), 14);

    uint8_t bitmapinfoheader[] = {
        0, 0, 0, 0,// the size of this header, in bytes
        0, 0, 0, 0, // the bitmap width in pixels
        0, 0, 0, 0, // the bitmap height in pixels
        0, 0, //the number of color planes
        0, 0, //the number of bits per pixel, which is the color depth of the image
        0, 0, 0, 0, //the compression method being used. See the next table for a list of possible values
        0, 0, 0, 0, // the image size. This is the size of the raw bitmap data; a dummy 0 can be given for BI_RGB bitmaps.
        0, 0, 0, 0, //the horizontal resolution of the image
        0, 0, 0, 0, //the vertical resolution of the image.
        0, 0, 0, 0, //the number of colors in the color palette, or 0 to default to 2n
        0, 0, 0, 0 //the number of important colors used, or 0 when every color is important; generally ignored
    };

    completion_bitmapinfoheader(bitmapinfoheader, width, height);

    file.write(reinterpret_cast<char*> (bitmapinfoheader), 40);

    uint8_t color_table[] {
        255, 255, 255, 0, //white
        255, 67, 90, 0, //blue
        46, 255 ,255, 0, //yellow
        255, 0, 255, 0, //purple
        87, 139, 46, 0 // green
    };

    file.write(reinterpret_cast<char*> (color_table), 20);

    uint8_t pixel;
    uint64_t sup_pixel;

    uint8_t padding[] = {0, 0, 0};

    for(int64_t y = static_cast<int64_t>(height) - 1; y >= 0; --y){
        for(int64_t x = 0; x < width; x += 2){
            pixel = field[y][x];
            if(pixel > 4){
                pixel = 4;
            }
            if(x + 1 >= width){
                sup_pixel = 4;

            }else{
                sup_pixel = field[y][x+1];
                if(sup_pixel > 4){
                    sup_pixel = 4;
                }
            }
            pixel = (pixel << 4) + static_cast<int8_t>(sup_pixel);

            file.write(reinterpret_cast<char*> (&pixel),1);
        }
        if(paddingSize!=0) {
            file.write(reinterpret_cast<char *> (padding), paddingSize);
        }
    }

#pragma pack(pop)
}
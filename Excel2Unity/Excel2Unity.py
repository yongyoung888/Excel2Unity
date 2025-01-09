
import os
# import xlrd
import pandas
from Config import EXCEL_DIR
from Config import EXCEL_EXT
from Config import UNITY_TABLE_FIELD_FILTER
from Config import UnityDataDir
from ConfigDataGen import ConfigDataGen
from UnityCodeGen import  UnityCodeGen

class Excel2Unity:
    # 构造函数
    def __init__(self):
        self.mExcelFiles = []  # 所有的excel文件

    # 外部处理函数
    def Process(self):
        self.RecursiveSearchExcel(EXCEL_DIR)
        self.ProcessExcelExportUnity()

    # 递归查找文件
    def RecursiveSearchExcel(self, path):
        for pathdir in os.listdir(path):  # 遍历当前目录
            fullpath = os.path.join(path, pathdir)

            if os.path.isdir(fullpath):
                self.RecursiveSearchExcel(fullpath)
            elif os.path.isfile(fullpath):
                if os.path.splitext(fullpath)[1] == EXCEL_EXT:
                    self.mExcelFiles.append(fullpath)

    # 处理excel文件
    def ProcessExcelExportUnity(self):

        allbytesdata = bytes()

        # 处理每个文件
        for filename in self.mExcelFiles:
            # data = xlrd.open_workbook(filename)
            data = pandas.read_excel(filename, engine='openpyxl')
            table = data #.sheets()[0]
            fields = self.FilterFieldData(table, UNITY_TABLE_FIELD_FILTER)

            # 数据
            cfgbytes = ConfigDataGen.Process(fields, table)
            allbytesdata += cfgbytes

            # 代码
            UnityCodeGen.Process(filename, fields, table)

        # 后处理
        ConfigDataGen.Save(allbytesdata, UnityDataDir)
        UnityCodeGen.GenConfigMangerCode(self.mExcelFiles)

    # 筛选字段数据
    def FilterFieldData(self, table, fieldfilter):
        fields = []
        columns = table.loc[0].values.tolist()
        for idx, col in enumerate(columns):
            for field in fieldfilter:
                if col == field:
                    fields.append(idx)

        # for index in range(table.ncols):
        #     row = table.cell(1, index).value
        #     for field in fieldfilter:
        #         if row == field:
        #             fields.append(index)

        return fields
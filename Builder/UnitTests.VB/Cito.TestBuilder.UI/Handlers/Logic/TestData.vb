
Imports System.Xml.Linq

Public Class TestData
    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----|-----+-----+
    '|  5  |  6  |  7  |  8  |
    '+-----|-----|-----+-----+
    '|  9  |  10 |  11 |  12 |
    '+-----+-----+-----+-----+
    Friend Shared tstData As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                            <head>
                                                <title>Document</title>
                                                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                            </head>
                                            <body>
                                                <table style="width: 100%;">
                                                    <colgroup>
                                                        <col/>
                                                        <col/>
                                                        <col/>
                                                        <col/>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <p>1</p>
                                                            </td>
                                                            <td>
                                                                <p>2</p>
                                                            </td>
                                                            <td>
                                                                <p>3</p>
                                                            </td>
                                                            <td>
                                                                <p>4</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <p>5</p>
                                                            </td>
                                                            <td>
                                                                <p>6</p>
                                                            </td>
                                                            <td>
                                                                <p>7</p>
                                                            </td>
                                                            <td>
                                                                <p>8</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <p>9</p>
                                                            </td>
                                                            <td>
                                                                <p>10</p>
                                                            </td>
                                                            <td>
                                                                <p>11</p>
                                                            </td>
                                                            <td>
                                                                <p>12</p>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </body>
                                        </html>


    '+-----+-----+-----+-----+
    '|  1  |     2     |  3  |
    '+-----|-----+-----+-----+
    '|  4  |  5  |  6  |  7  |
    '+-----|-----|-----+-----+
    '|  8  |  9  |  10 |  11 |
    '+-----+-----+-----+-----+
    Friend Shared tstDataColspan As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                   <head>
                                                       <title>Document</title>
                                                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                   </head>
                                                   <body>
                                                       <table style="width: 100%;">
                                                           <colgroup>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                           </colgroup>
                                                           <tbody>
                                                               <tr>
                                                                   <td>
                                                                       <p>1</p>
                                                                   </td>
                                                                   <td colspan="2">
                                                                       <p>2</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>3</p>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td>
                                                                       <p>4</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>5</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>6</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>7</p>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td>
                                                                       <p>8</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>9</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>10</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>11</p>
                                                                   </td>
                                                               </tr>
                                                           </tbody>
                                                       </table>
                                                   </body>
                                               </html>

    '+-----+-----+-----+-----+
    '|     |  2  |  3  |  4  |
    '+  1  +-----+-----+-----+
    '|     |  5  |     |  7  |
    '+-----|-----+  6  +-----+
    '|  8  |  9  |     |  10 |
    '+-----+-----+-----+-----+
    Friend Shared tstDataRowspan As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                   <head>
                                                       <title>Document</title>
                                                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                   </head>
                                                   <body>
                                                       <table style="width: 100%;">
                                                           <colgroup>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                           </colgroup>
                                                           <tbody>
                                                               <tr>
                                                                   <td rowspan="2">
                                                                       <p>1</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>2</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>3</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>4</p>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td>
                                                                       <p>5</p>
                                                                   </td>
                                                                   <td rowspan="2">
                                                                       <p>6</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>7</p>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td>
                                                                       <p>8</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>9</p>
                                                                   </td>
                                                                   <td>
                                                                       <p>10</p>
                                                                   </td>
                                                               </tr>
                                                           </tbody>
                                                       </table>
                                                   </body>
                                               </html>

    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|     |  5  |     |
    '+     |-----+     +
    '|     |  6  |     |
    '+  4  |-----+  9  +
    '|     |  7  |     |
    '+     |-----+     +
    '|     |  8  |     |
    '+-----|-----+-----+
    '| 10  |  11 |  12 |
    '+-----+-----+-----+
    Friend Shared tstDataRowspan3x6 As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                      <head>
                                                          <title>Document</title>
                                                          <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                      </head>
                                                      <body>
                                                          <table style="width: 100%;">
                                                              <colgroup>
                                                                  <col/>
                                                                  <col/>
                                                                  <col/>
                                                              </colgroup>
                                                              <tbody>
                                                                  <tr>
                                                                      <td>
                                                                          <p>1</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>2</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>3</p>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td rowspan="4">
                                                                          <p>4</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>5</p>
                                                                      </td>
                                                                      <td rowspan="4">
                                                                          <p>9</p>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td><p>6</p></td>
                                                                  </tr><tr>
                                                                      <td><p>7</p></td>
                                                                  </tr><tr>
                                                                      <td><p>8</p></td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td>
                                                                          <p>10</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>11</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>12</p>
                                                                      </td>
                                                                  </tr>
                                                              </tbody>
                                                          </table>
                                                      </body>
                                                  </html>

    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|     |  5  |     |
    '+     |-----+     +
    '|     |  6  |     |
    '+     |-----+     +
    '|  4  |  7  | 10  |
    '+     |-----+     +
    '|     |  8  |     |
    '+     |-----+     +
    '|     |  9  |     |
    '+-----|-----+-----+
    '| 11  |  12 |  13 |
    '+-----+-----+-----+
    Friend Shared tstDataRowspan3x7 As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                      <head>
                                                          <title>Document</title>
                                                          <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                      </head>
                                                      <body>
                                                          <table style="width: 100%;">
                                                              <colgroup>
                                                                  <col/>
                                                                  <col/>
                                                                  <col/>
                                                              </colgroup>
                                                              <tbody>
                                                                  <tr>
                                                                      <td>
                                                                          <p>1</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>2</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>3</p>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td rowspan="5">
                                                                          <p>4</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>5</p>
                                                                      </td>
                                                                      <td rowspan="5">
                                                                          <p>10</p>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td><p>6</p></td>
                                                                  </tr><tr>
                                                                      <td><p>7</p></td>
                                                                  </tr><tr>
                                                                      <td><p>8</p></td>
                                                                  </tr><tr>
                                                                      <td><p>9</p></td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td>
                                                                          <p>11</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>12</p>
                                                                      </td>
                                                                      <td>
                                                                          <p>13</p>
                                                                      </td>
                                                                  </tr>
                                                              </tbody>
                                                          </table>
                                                      </body>
                                                  </html>

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----+-----+-----+
    '|  5  |           |  7  |
    '+-----|     6     |-----+
    '|  8  |           |  9  |
    '+-----+-----+-----+-----+
    '|  10 |  11 |  12 |  13 |
    '+-----+-----+-----+-----+
    Friend Shared tstData4x4 As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                               <head>
                                                   <title>Document</title>
                                                   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                               </head>
                                               <body>
                                                   <table style="width: 100%;">
                                                       <colgroup>
                                                           <col/>
                                                           <col/>
                                                           <col/>
                                                           <col/>
                                                       </colgroup>
                                                       <tbody>
                                                           <tr>
                                                               <td>
                                                                   <p>1</p>
                                                               </td>
                                                               <td>
                                                                   <p>2</p>
                                                               </td>
                                                               <td>
                                                                   <p>3</p>
                                                               </td>
                                                               <td>
                                                                   <p>4</p>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td>
                                                                   <p>5</p>
                                                               </td>
                                                               <td colspan="2" rowspan="2">
                                                                   <p>6</p>
                                                               </td>
                                                               <td>
                                                                   <p>7</p>
                                                               </td>
                                                           </tr>
                                                           <tr>
                                                               <td><p>8</p></td>
                                                               <td><p>9</p></td>
                                                           </tr>
                                                           <tr>
                                                               <td>
                                                                   <p>10</p>
                                                               </td>
                                                               <td>
                                                                   <p>11</p>
                                                               </td>
                                                               <td>
                                                                   <p>12</p>
                                                               </td>
                                                               <td>
                                                                   <p>13</p>
                                                               </td>
                                                           </tr>
                                                       </tbody>
                                                   </table>
                                               </body>
                                           </html>


    '+----+----+----+----+
    '|  1 |    |    |    |
    '+----+    | [1]|    |
    '|r[3]|    |    |    |
    '+----+  2 +----+  3 |
    '|r[2]|    |    |    |
    '+----+    |r[4]|    |
    '|r[1]|    |    |    |
    '+-------------------+
    '|  4 |    5    |  6 |
    '+-------------------+
    '|  7 |    8    |  9 |
    '+-------------------+
    Friend Shared problem1 As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                             <head>
                                                 <title>Document</title>
                                                 <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                             </head>
                                             <body>
                                                 <table style="width: 100%;">
                                                     <colgroup>
                                                         <col/>
                                                         <col/>
                                                         <col/>
                                                         <col/>
                                                     </colgroup>
                                                     <tbody>
                                                         <tr>
                                                             <td>
                                                                 <p>1</p>
                                                             </td>
                                                             <td rowspan="4">
                                                                 <p>2</p>
                                                             </td>
                                                             <td rowspan="2">
                                                                 <p>[1]</p>
                                                             </td>
                                                             <td rowspan="4">
                                                                 <p>3</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 <p>r[3]</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 <p>r[2]</p>
                                                             </td>
                                                             <td rowspan="2">
                                                                 <p>r[4]</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 <p>r[1]</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 <p>4</p>
                                                             </td>
                                                             <td colspan="2">
                                                                 <p>5</p>
                                                             </td>
                                                             <td>
                                                                 <p>6</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 <p>7</p>
                                                             </td>
                                                             <td colspan="2">
                                                                 <p>8</p>
                                                             </td>
                                                             <td>
                                                                 <p>9</p>
                                                             </td>
                                                         </tr>
                                                     </tbody>
                                                 </table>
                                             </body>
                                         </html>


    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|           |  5  |
    '+     4     +-----|
    '|           |  6  |
    '+-----+-----+-----+    
    Friend Shared problem2 As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                             <head>
                                                 <title>Document Title 2</title>
                                                 <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                             </head>
                                             <body>
                                                 <table style="width: 100%;">
                                                     <colgroup>
                                                         <col/>
                                                         <col/>
                                                         <col/>
                                                     </colgroup>
                                                     <tbody>
                                                         <tr>
                                                             <td>
                                                                 <p>1</p>
                                                             </td>
                                                             <td>
                                                                 <p>2</p>
                                                             </td>
                                                             <td>
                                                                 <p>3</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td rowspan="2" colspan="2">
                                                                 <p>4</p>
                                                             </td>
                                                             <td>
                                                                 <p>5</p>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 <p>6</p>
                                                             </td>
                                                         </tr>
                                                     </tbody>
                                                 </table>
                                             </body>

                                         </html>


    'Single row with interesting tags.
    '<td/>
    '<td>text</td>
    '<td><p>1</p></td>
    '<td><p>2</p><p>3</p></td>
    '<td><p>4</p>out of p tag<p>5</p></td>
    Friend Shared tableMergeData As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                   <head>
                                                       <title>Document Title 2</title>
                                                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                   </head>
                                                   <body>
                                                       <table style="width: 100%;">
                                                           <colgroup>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                               <col/>
                                                           </colgroup>
                                                           <tbody>
                                                               <tr>
                                                                   <td/>
                                                                   <td>text</td>
                                                                   <td><p>1</p></td>
                                                                   <td><p>2</p><p>3</p></td>
                                                                   <td><p>4</p>out of p tag<p>5</p></td>
                                                               </tr>
                                                           </tbody>
                                                       </table>
                                                   </body>
                                               </html>

    Friend Shared DocumentWith2_2x2Tables As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                            <head>
                                                                <title>Document Title 2</title>
                                                                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                            </head>
                                                            <body>
                                                                <table style="width: 100%;">
                                                                    <colgroup>
                                                                        <col/>
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>1</td>
                                                                        </tr><tr>
                                                                            <td>2</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <br/>
                                                                <table style="width: 100%;">
                                                                    <colgroup>
                                                                        <col/>
                                                                        <col/>
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>a</td>
                                                                        </tr><tr>
                                                                            <td>b</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </body>
                                                        </html>

    Friend Shared DocumentWith2Tables_3x1_1x2 As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                                                <head>
                                                                    <title>Document Title 2</title>
                                                                    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                                                </head>
                                                                <body>
                                                                    <table style="width: 100%;">
                                                                        <colgroup>
                                                                            <col/>
                                                                            <col/>
                                                                            <col/>
                                                                        </colgroup>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>1</td>
                                                                                <td>2</td>
                                                                                <td>3</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <br/>
                                                                    <table style="width: 100%;">
                                                                        <colgroup>
                                                                            <col/>
                                                                        </colgroup>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>a</td>
                                                                            </tr><tr>
                                                                                <td>b</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </body>
                                                            </html>

End Class

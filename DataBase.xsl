<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"></xsl:output>
    <xsl:template match="/">
      <html>
        <body>
          <table border="1">
            <TR>
              <th>City</th>
              <th>Cinema</th>
              <th>Movie</th>
              <th>Date</th>
              <th>Time</th>
              <th>Price</th>
            </TR>
            <xsl:for-each select ="FilmsDataBase/film">
              <tr>
                <td>
                  <xsl:value-of select ="@CITY"/>
                </td>
                <td>
                  <xsl:value-of select ="@CINEMA"/>
                </td>
                <td>
                  <xsl:value-of select ="@MOVIE"/>
                </td>
                <td>
                  <xsl:value-of select ="@DATE"/>
                </td>
                <td>
                  <xsl:value-of select ="@TIME"/>
                </td>
                <td>
                  <xsl:value-of select ="@PRICE"/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>

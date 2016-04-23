<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/*">
    <table class="table table-striped">
      <tr>
        <th>Kod</th>
        <th>Nazwa</th>
        <th>Kurs średni</th>
      </tr>
      <xsl:for-each select="pozycja">
        <tr>
          <td>
            <xsl:value-of select="kod_waluty"></xsl:value-of>
          </td>
          <td>
            <xsl:value-of select="nazwa_waluty"></xsl:value-of>
          </td>
          <td>
            <xsl:value-of select="kurs_sredni"></xsl:value-of>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>

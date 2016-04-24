<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/*">
    <html>
      <body>
        <p>Zamówienia zostały opłacone!</p>
        <p>Nie odpowiadaj na te meila! Został on wygenerowany z użyciem najnowszej technologii!</p>
        
        <h1>Zamówienia</h1>
        <table class="table table-hover">
          <tr>
            <th>Order ID</th>
            <th>Amount</th>
          </tr>
          <xsl:for-each select="Order">
            <tr>
              <td>
                <xsl:value-of select="Id"></xsl:value-of>
              </td>
              <td>
                <xsl:value-of select="Amount"></xsl:value-of>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

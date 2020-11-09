<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output
		method="html"></xsl:output>
	<xsl:template match="/">
		<html>
			<body>
				<table border ="1">
					<TR>
						<th>Title</th>
						<th>Date</th>
						<th>PriceRange</th>
						<th>Location</th>
						<th>Commentators</th>
						<th>Participants</th>
						<th>Type</th>
					</TR>
					<xsl:for-each select="Tournaments/Tournament">
						<tr>
							<td>
								<xsl:value-of select="@Title"/>
							</td>
							<td>
								<xsl:value-of select="@Date"/>
							</td>
							<td>
								<xsl:value-of select="@PriceRange"/>
							</td>
							<td>
								<xsl:value-of select="@Location"/>
							</td>
							<td>
								<xsl:value-of select="@Commentators"/>
							</td>
							<td>
								<xsl:value-of select="@Participants"/>
							</td>
							<td>
								<xsl:value-of select="@Type"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
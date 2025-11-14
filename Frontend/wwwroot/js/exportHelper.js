// Export helper functions for PDF and Excel
window.exportHelper = {
    exportToPDF: function (title, data) {
        try {
            const { jsPDF } = window.jspdf;
            const doc = new jsPDF();

            // Add title
            doc.setFontSize(18);
            doc.text(title, 14, 22);

            // Add date
            const now = new Date();
            doc.setFontSize(10);
            doc.text('Ngày xuất: ' + now.toLocaleDateString('vi-VN'), 14, 30);

            let yPos = 40;
            const pageHeight = doc.internal.pageSize.height;
            const margin = 14;
            const lineHeight = 7;

            // Add data
            doc.setFontSize(12);
            if (data.headers && data.rows) {
                // Table format
                const headers = data.headers;
                const rows = data.rows;

                // Calculate column widths
                const pageWidth = doc.internal.pageSize.width;
                const colWidth = (pageWidth - 2 * margin) / headers.length;

                // Draw headers
                doc.setFont(undefined, 'bold');
                headers.forEach((header, i) => {
                    doc.text(header, margin + i * colWidth, yPos);
                });
                yPos += lineHeight;

                // Draw rows
                doc.setFont(undefined, 'normal');
                rows.forEach(row => {
                    if (yPos > pageHeight - 20) {
                        doc.addPage();
                        yPos = margin;
                    }
                    row.forEach((cell, i) => {
                        const cellText = String(cell || '');
                        doc.text(cellText, margin + i * colWidth, yPos);
                    });
                    yPos += lineHeight;
                });
            } else if (data.text) {
                // Simple text format
                const lines = doc.splitTextToSize(data.text, pageWidth - 2 * margin);
                lines.forEach(line => {
                    if (yPos > pageHeight - 20) {
                        doc.addPage();
                        yPos = margin;
                    }
                    doc.text(line, margin, yPos);
                    yPos += lineHeight;
                });
            }

            // Save the PDF
            doc.save(title.replace(/[^a-z0-9]/gi, '_') + '.pdf');
            return true;
        } catch (error) {
            console.error('Error exporting to PDF:', error);
            return false;
        }
    },

    exportToExcel: function (title, data) {
        try {
            if (!window.XLSX) {
                console.error('XLSX library not loaded');
                return false;
            }

            const wb = XLSX.utils.book_new();

            if (data.sheets && Array.isArray(data.sheets)) {
                // Multiple sheets
                data.sheets.forEach(sheet => {
                    if (sheet.headers && sheet.rows) {
                        const wsData = [sheet.headers, ...sheet.rows];
                        const ws = XLSX.utils.aoa_to_sheet(wsData);
                        
                        // Set column widths
                        if (sheet.headers.length > 0) {
                            const colWidths = sheet.headers.map((_, i) => {
                                const maxLength = Math.max(
                                    ...wsData.map(row => String(row[i] || '').length)
                                );
                                return { wch: Math.min(maxLength + 2, 50) };
                            });
                            ws['!cols'] = colWidths;
                        }
                        
                        XLSX.utils.book_append_sheet(wb, ws, sheet.name || 'Sheet1');
                    }
                });
            } else if (data.headers && data.rows) {
                // Single sheet
                const wsData = [data.headers, ...data.rows];
                const ws = XLSX.utils.aoa_to_sheet(wsData);

                // Set column widths
                const colWidths = data.headers.map((_, i) => {
                    const maxLength = Math.max(
                        ...wsData.map(row => String(row[i] || '').length)
                    );
                    return { wch: Math.min(maxLength + 2, 50) };
                });
                ws['!cols'] = colWidths;

                XLSX.utils.book_append_sheet(wb, ws, 'Báo cáo');
            }

            // Save the file
            XLSX.writeFile(wb, title.replace(/[^a-z0-9]/gi, '_') + '.xlsx');
            return true;
        } catch (error) {
            console.error('Error exporting to Excel:', error);
            return false;
        }
    }
};


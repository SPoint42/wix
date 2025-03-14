// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Core.Link
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WixToolset.Data;
    using WixToolset.Data.Symbols;

    /// <summary>
    /// Symbol with section representing a single unique symbol.
    /// </summary>
    internal class SymbolWithSection
    {
        private List<WixSimpleReferenceSymbol> directReferences;
        private HashSet<SymbolWithSection> possibleConflicts;

        /// <summary>
        /// Creates a symbol for a symbol.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="symbol">Symbol for the symbol</param>
        public SymbolWithSection(IntermediateSection section, IntermediateSymbol symbol)
        {
            this.Symbol = symbol;
            this.Section = section;
        }

        /// <summary>
        /// Gets the accessibility of the symbol which is a direct reflection of the accessibility of the row's accessibility.
        /// </summary>
        /// <value>Accessbility of the symbol.</value>
        public AccessModifier Access => this.Symbol.Id.Access;

        /// <summary>
        /// Gets the symbol for this symbol.
        /// </summary>
        /// <value>Symbol for this symbol.</value>
        public IntermediateSymbol Symbol { get; }

        /// <summary>
        /// Gets the section for the symbol.
        /// </summary>
        /// <value>Section for the symbol.</value>
        public IntermediateSection Section { get; }

        /// <summary>
        /// Gets any duplicates of this symbol with sections that are possible conflicts.
        /// </summary>
        public IEnumerable<WixSimpleReferenceSymbol> DirectReferences => this.directReferences ?? Enumerable.Empty<WixSimpleReferenceSymbol>();

        /// <summary>
        /// Gets any duplicates of this symbol with sections that are possible conflicts.
        /// </summary>
        public IEnumerable<SymbolWithSection> PossiblyConflicts => this.possibleConflicts ?? Enumerable.Empty<SymbolWithSection>();

        /// <summary>
        ///  Gets the virtual symbol that is overridden by this symbol.
        /// </summary>
        public SymbolWithSection Overrides { get; private set; }

        /// <summary>
        /// Adds a duplicate symbol with sections that is a possible conflict.
        /// </summary>
        /// <param name="symbolWithSection">Symbol with section that is a possible conflict of this symbol.</param>
        public void AddPossibleConflict(SymbolWithSection symbolWithSection)
        {
            if (null == this.possibleConflicts)
            {
                this.possibleConflicts = new HashSet<SymbolWithSection>();
            }

            this.possibleConflicts.Add(symbolWithSection);
        }

        /// <summary>
        /// Adds a reference that directly points to this symbol.
        /// </summary>
        /// <param name="reference">The direct reference.</param>
        public void AddDirectReference(WixSimpleReferenceSymbol reference)
        {
            if (null == this.directReferences)
            {
                this.directReferences = new List<WixSimpleReferenceSymbol>();
            }

            this.directReferences.Add(reference);
        }

        /// <summary>
        /// Override a virtual symbol.
        /// </summary>
        /// <param name="virtualSymbolWithSection">Virtual symbol with section that is being overridden.</param>
        public void OverrideVirtualSymbol(SymbolWithSection virtualSymbolWithSection)
        {
            if (virtualSymbolWithSection.Access != AccessModifier.Virtual)
            {
                throw new InvalidOperationException("Cannot override non-virtual symbols");
            }

            this.Overrides = virtualSymbolWithSection;
        }

        /// <summary>
        /// Gets the full name of the symbol.
        /// </summary>
        public string GetFullName()
        {
            return String.Concat(this.Symbol.Definition.Name, ":", this.Symbol.Id.Id);
        }
    }
}
